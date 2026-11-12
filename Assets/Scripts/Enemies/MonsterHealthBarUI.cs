/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Actualización de la barra de vida del monstruo.
 * Explicación: Lee la vida actual del monstruo y sincroniza un Slider
 *              de Unity para mostrarla en pantalla.
 *********************************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBarUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private MonsterHealth monsterHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        if (monsterHealth == null)
        {
            monsterHealth =
                FindFirstObjectByType<MonsterHealth>();
        }

        if (monsterHealth != null &&
            healthSlider != null)
        {
            healthSlider.maxValue =
                monsterHealth.MaxHealth;

            healthSlider.value =
                monsterHealth.CurrentHealth;
        }
    }

    private void Update()
    {
        if (monsterHealth == null ||
            healthSlider == null)
            return;

        healthSlider.value =
            monsterHealth.CurrentHealth;
    }
}