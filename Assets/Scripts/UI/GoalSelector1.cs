/**********
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Lee la selección del menú de metas.
 * Explicación: Toma el valor elegido en el TMP_Dropdown y lo
 * guarda para que la escena de juego pueda utilizarlo.
 *******************************/

using TMPro;
using UnityEngine;

public class GoalSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown goalDropdown;

    public void SaveSelectedGoal()
    {
        switch (goalDropdown.value)
        {
            case 0:
                GameSettings.SelectedGoalPercentage = 20f;
                break;

            case 1:
                GameSettings.SelectedGoalPercentage = 40f;
                break;

            case 2:
                GameSettings.SelectedGoalPercentage = 60f;
                break;

            case 3:
                GameSettings.SelectedGoalPercentage = 80f;
                break;

            case 4:
                GameSettings.SelectedGoalPercentage = 100f;
                break;
        }

        Debug.Log(
            $"Meta seleccionada: {GameSettings.SelectedGoalPercentage}%");
    }
}