/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Contenedor de datos configurables para cada tipo de contaminante.
 * Explicación: Centraliza la información visual y los valores asociados
 *              a la recolección de contaminantes, desacoplando los datos
 *              de la lógica del juego.
 *********************************************************************************************/

using UnityEngine;

[CreateAssetMenu(fileName = "NuevoContaminante", menuName = "LimpiezaMarina/Contaminant Data")]
public class ContaminantDataSO : ScriptableObject
{
    [Header("Identificación")]
    public string contaminantName;

    [TextArea]
    public string description;

    [Header("Visual")]
    public Sprite icon;

    [Header("Progreso")]
    [Tooltip("Porcentaje de limpieza que aporta al ser recogido.")]
    public float cleaningValue = 1f;

    [Header("Puntaje")]
    public int scoreValue = 100;
}