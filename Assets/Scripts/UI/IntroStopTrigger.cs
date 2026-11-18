/**********
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Disparo de la parada de Lumi en la pantalla inicial.
 * Explicación: Cuando Lumi entra al trigger, se activa el trigger
 *              "Parar" del Animator y se ajusta su rotación final.
 *******************************/

using UnityEngine;

public class IntroStopTrigger : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Animator lumiAnimator;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (!other.CompareTag("Player"))
            return;

        activated = true;

        if (lumiAnimator != null)
        {
            lumiAnimator.SetTrigger("Parar");

            lumiAnimator.transform.rotation =
                Quaternion.Euler(0f, 288f, 0f);
        }
    }
}