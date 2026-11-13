/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Contenedor de datos de una meta de limpieza seleccionable.
 * Explicación: Define el nombre y el porcentaje de limpieza que el jugador
 *              debe alcanzar para ganar la partida.
 *********************************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "NuevaMeta", menuName = "LimpiezaMarina/Cleaning Goal")]
public class CleaningGoalSO : ScriptableObject
{
    [Header("Identificación")]
    public string goalName;

    [TextArea]
    public string description;

    [Header("Meta")]
    [Range(1f, 100f)]
    public float targetPercentage = 100f;
}