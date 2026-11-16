/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Disparar una animación mediante un trigger.
 * Explicación: Al iniciar la escena envía al Animator
 *              el trigger configurado desde el Inspector.
 *********************************************************************************************/

using UnityEngine;

public class TriggerAnimatorOnStart : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Animator animatorObjetivo;

    [Header("Animación")]
    [SerializeField] private string triggerName;

    [Header("Opcional")]
    [SerializeField] private float delay = 0f;

    private void Start()
    {
        if (delay <= 0f)
        {
            LanzarTrigger();
        }
        else
        {
            Invoke(nameof(LanzarTrigger), delay);
        }
    }

    private void LanzarTrigger()
    {
        if (animatorObjetivo == null)
        {
            Debug.LogWarning(
                "[TriggerAnimatorOnStart] No se asignó Animator.");
            return;
        }

        animatorObjetivo.SetTrigger(triggerName);
    }
}