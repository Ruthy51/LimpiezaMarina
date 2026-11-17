/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestión centralizada del progreso de limpieza ambiental.
 * Explicación: Calcula el valor total de los contaminantes presentes
 *              en la escena (equivalente al 100%), acumula lo recogido,
 *              aplica la meta elegida por el jugador y avisa cuando se
 *              alcanza para activar la victoria.
 *********************************************************************************************/


using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;

    public void ToggleState()
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
    }
}