/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Control del proyectil de burbuja del Dron y depuración de impactos contra enemigos.
 * Explicación: Desplaza la burbuja en el espacio, imprime trazas detalladas de colisión en la consola (Debug.Log)
 *              y aplica daño al enemigo si cuenta con el Tag 'Enemy' e implementa la interfaz IDamageable.
 *********************************************************************************************/

using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float damageAmount = 15f;

    private float timer;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log($"<color=cyan>[Burbuja Debug]</color> Trigger con: '{other.gameObject.name}' | Layer: '{LayerMask.LayerToName(other.gameObject.layer)}' | Tag: '{other.tag}'");

        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<IDamageable>(out IDamageable target))
            {
                Debug.Log($"<color=green>[Burbuja Éxito]</color> Impacto en '{other.gameObject.name}'. Daño: {damageAmount}");
                target.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning($"[Burbuja Alerta] Objeto '{other.gameObject.name}' tiene Tag Enemy pero NO implementa IDamageable.");
            }

            gameObject.SetActive(false);
        }
    }
}