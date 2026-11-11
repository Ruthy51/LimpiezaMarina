/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Controlador principal del enemigo para coordinar la evaluación de rango y ejecución de ataques.
 * Explicación: Persigue al jugador, utiliza petróleo a distancia mientras
 *              continúa avanzando y realiza una embestida cuando entra
 *              en rango melee.
 *********************************************************************************************/

using UnityEngine;

public class AquaticsEnemyController : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform droneTarget;

    [Header("Configuración de Movimiento")]
    [SerializeField] private float swimSpeed = 1.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float stoppingDistance = 6.0f;

    [Header("Rangos de Ataque")]
    [SerializeField] private float meleeRange = 3.5f;
    [SerializeField] private float rangedRange = 8.0f;

    [Header("Componentes de Ataque")]
    [SerializeField] private EnemyMeleeAttack meleeAttack;
    [SerializeField] private EnemyRangedAttack rangedAttack;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    private Collider enemyCollider;
    private CharacterController droneController;

    private Vector3 ultimaPosicion;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (animator == null)
            animator = GetComponent<Animator>();

        enemyCollider = GetComponent<Collider>();

        if (droneTarget == null)
        {
            GameObject playerObj =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                droneTarget = playerObj.transform;
            }
        }

        if (droneTarget != null)
        {
            droneController =
                droneTarget.GetComponent<CharacterController>();
        }
    }

    private void FixedUpdate()
    {
        if (droneTarget == null)
            return;

        float distance = GetCombatDistance();

           RotateTowardsTarget();

        // PRIORIDAD MÁXIMA: MELEE
        if (distance <= meleeRange)
        {
    
            if (rangedAttack != null)
            {
                rangedAttack.BlockRangedAttack();
            }

            if (animator != null)
            {
                animator.SetBool("IsSwimming", false);
            }

            if (meleeAttack != null &&
                meleeAttack.CanAttack())
            {
  
                meleeAttack.ExecuteMeleeAttack(
                    droneTarget.gameObject);
            }

            return;
        }

        // Vuelve a permitir ranged al salir de melee
        if (rangedAttack != null)
        {
            rangedAttack.UnblockRangedAttack();
        }

        // PRIORIDAD SECUNDARIA: RANGED
        if (distance <= rangedRange)
        {
 
            if (animator != null)
            {
                animator.SetBool("IsSwimming", true);
            }

            MoveForward();

            if (rangedAttack != null &&
                rangedAttack.CanAttack())
            {
 
                rangedAttack.ExecuteAttack();
            }

            return;
        }

         MoveForward();

        if (animator != null)
        {
            animator.SetBool("IsSwimming", true);
        }
    }

    private float GetCombatDistance()
    {
        if (enemyCollider == null ||
            droneController == null)
        {
            return Vector3.Distance(
                transform.position,
                droneTarget.position);
        }

        Vector3 enemyPoint =
            enemyCollider.ClosestPoint(
                droneTarget.position);

        Vector3 dronePoint =
            droneController.bounds.ClosestPoint(
                transform.position);

        return Vector3.Distance(
            enemyPoint,
            dronePoint);
    }

    private void MoveForward()
    {
        Vector3 targetPosition =
            Vector3.MoveTowards(
                rb.position,
                droneTarget.position,
                swimSpeed * Time.fixedDeltaTime);

         rb.MovePosition(targetPosition);
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction =
            (droneTarget.position - transform.position).normalized;

        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, ultimaPosicion) > 5f)
        {
         }

        ultimaPosicion = transform.position;
    }
}