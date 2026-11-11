/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Controlador principal del jugador (Aqua-Drone).
 * Explicación: Gestiona movimiento, orientación mediante mouse,
 * disparo de burbujas y activación temporal del cono recolector.
 *********************************************************************************************/

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController3D : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Referencias de Ataque")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject scannerConeObject;
    [SerializeField] private ObjectPool bubblePool;

    [Header("Visor")]
    [SerializeField] private Transform visor;
    [SerializeField] private float visorDistance = 1.2f;

    [Header("Configuración del Cono")]
    [SerializeField] private float scannerDuration = 0.3f;

    private CharacterController characterController;
    private Camera mainCamera;
    private bool isScanning = false;

    private Vector3 currentMoveDirection;
    private Vector3 visorIdlePosition;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        if (visor != null)
        {
            visorIdlePosition = visor.localPosition;
        }

        if (scannerConeObject != null)
        {
            scannerConeObject.SetActive(false);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleRotationToMouse();
        UpdateVisorPosition();
        HandleAttacks();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection =
            new Vector3(horizontal, 0f, vertical).normalized;

        currentMoveDirection = moveDirection;

        if (moveDirection.magnitude >= 0.1f)
        {
            characterController.Move(
                moveDirection * moveSpeed * Time.deltaTime);
        }
    }

private void UpdateVisorPosition()
{
    if (visor == null)
        return;

    // Radio real de la órbita sobre la esfera
    float radius = Mathf.Abs(visorIdlePosition.z);

    // Quieto = vuelve suavemente al frente
    if (currentMoveDirection.sqrMagnitude < 0.01f)
    {
        visor.localPosition =
            Vector3.Lerp(
                visor.localPosition,
                visorIdlePosition,
                10f * Time.deltaTime);

        return;
    }

    Vector3 localMoveDirection =
        transform.InverseTransformDirection(
            currentMoveDirection);

    localMoveDirection.y = 0f;

    if (localMoveDirection.sqrMagnitude < 0.001f)
        return;

    localMoveDirection.Normalize();

    Vector3 targetPosition =
        new Vector3(
            localMoveDirection.x * radius,
            visorIdlePosition.y,
            localMoveDirection.z * radius);

    visor.localPosition =
        Vector3.Lerp(
            visor.localPosition,
            targetPosition,
            12f * Time.deltaTime);
}
    private void HandleRotationToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Plane waterPlane =
            new Plane(
                Vector3.up,
                new Vector3(
                    0,
                    transform.position.y,
                    0));

        if (waterPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 pointToLook =
                ray.GetPoint(rayDistance);

            Vector3 lookDirection =
                pointToLook - transform.position;

            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.001f)
            {
                transform.rotation =
                    Quaternion.LookRotation(
                        lookDirection);
            }
        }
    }

    private void HandleAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ExecuteRangedAttack();
        }

        if ((Input.GetMouseButtonDown(1) ||
             Input.GetKeyDown(KeyCode.Space))
             && !isScanning)
        {
            StartCoroutine(ActivateCollectorCone());
        }
    }

    private void ExecuteRangedAttack()
    {
        if (bubblePool == null || firePoint == null)
            return;

        GameObject bubble =
            bubblePool.GetPooledObject();

        if (bubble != null)
        {
            bubble.transform.position =
                firePoint.position;

            bubble.transform.rotation =
                firePoint.rotation;

            bubble.SetActive(true);
        }
    }

    private IEnumerator ActivateCollectorCone()
    {
        isScanning = true;

        if (scannerConeObject != null)
        {
            scannerConeObject.SetActive(true);
        }

        yield return new WaitForSeconds(
            scannerDuration);

        if (scannerConeObject != null)
        {
            scannerConeObject.SetActive(false);
        }

        isScanning = false;
    }
}