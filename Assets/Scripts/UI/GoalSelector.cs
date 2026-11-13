/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Permite elegir la meta de limpieza desde la interfaz.
 * Explicación: Expone un método público para que los botones de la pantalla de
 *              Inicio registren la meta elegida en GameSession.
 *********************************************************************************************/

using UnityEngine;

public class GoalSelector : MonoBehaviour
{
    public void SelectGoal(CleaningGoalSO goal)
    {
        GameSession.SelectGoal(goal);
    }
}