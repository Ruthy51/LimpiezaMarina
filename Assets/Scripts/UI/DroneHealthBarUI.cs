/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Actualización de la barra de energía del AquaDrone.
 * Explicación: Lee la vida actual del dron y sincroniza un Slider
 *              de Unity para mostrarla en pantalla.
 *********************************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class DroneHealthBarUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AquaDrone aquaDrone;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        if (aquaDrone == null)
        {
            aquaDrone =
                FindFirstObjectByType<AquaDrone>();
        }

        if (aquaDrone != null &&
            healthSlider != null)
        {
            healthSlider.maxValue =
                aquaDrone.MaxHealth;

            healthSlider.value =
                aquaDrone.CurrentHealth;
        }
    }

    private void Update()
    {
        if (aquaDrone == null ||
            healthSlider == null)
            return;

        healthSlider.value =
            aquaDrone.CurrentHealth;
    }
}