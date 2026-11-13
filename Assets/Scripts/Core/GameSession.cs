/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Conserva la meta de limpieza elegida entre escenas.
 * Explicación: Clase estática que guarda la meta seleccionada en la pantalla de
 *              Inicio para que el CleaningManager la lea al cargar el nivel.
 *********************************************************************************************/

public static class GameSession
{
    public static CleaningGoalSO SelectedGoal { get; private set; }

    public static void SelectGoal(CleaningGoalSO goal)
    {
        SelectedGoal = goal;
    }
}