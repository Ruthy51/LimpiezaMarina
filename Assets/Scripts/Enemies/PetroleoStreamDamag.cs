/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Aplicación de contaminación por contacto de partículas de petróleo.
 * Explicación: Utiliza el evento OnParticleCollision para detectar
 *              impactos sobre objetos que implementen IDamageable.
 *              Aplica daño continuo representando exposición a la
 *              contaminación generada por el monstruo.
 *********************************************************************************************/

using UnityEngine;

public class PetroleoStreamDamage : MonoBehaviour
{
    [Header("Configuración de Contaminación")]
    [SerializeField] private float contaminationDamage = 5f;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(
            "[PetroleoStream] COLISION CON: "
            + other.name);

        IDamageable damageable =
            other.GetComponent<IDamageable>();

        if (damageable == null)
        {
            damageable =
                other.GetComponentInParent<IDamageable>();
        }

        if (damageable != null)
        {
            damageable.TakeDamage(contaminationDamage);

            Debug.Log(
                $"[PetroleoStream] Impacto en '{other.name}'. " +
                $"Contaminación aplicada: {contaminationDamage}");
        }
    }
}