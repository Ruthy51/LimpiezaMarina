/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestión centralizada del progreso de limpieza ambiental.
 * Explicación: Calcula el valor total de los contaminantes presentes en la escena
 *              (equivalente al 100%), acumula lo recogido, aplica la meta elegida
 *              por el jugador y avisa cuando se alcanza para activar la victoria.
 *********************************************************************************************/

using System;
using UnityEngine;

public class CleaningManager : MonoBehaviour
{
    public static CleaningManager Instance { get; private set; }

    [Header("Progreso (solo lectura en juego)")]
    [SerializeField] private float collectedValue;
    [SerializeField] private float totalValue;

    [Header("Meta")]
    [Tooltip("Se usa solo si la escena se juega directo, sin pasar por la pantalla de Inicio.")]
    [SerializeField] private CleaningGoalSO defaultGoal;

    private float goalPercentage = 100f;
    private bool goalReached;

    // Margen para evitar errores de redondeo al sumar decimales.
    private const float GoalTolerance = 0.01f;

    public float CurrentCleaningPercentage =>
        totalValue > 0f ? Mathf.Clamp(collectedValue / totalValue * 100f, 0f, 100f) : 0f;

    public float GoalPercentage => goalPercentage;
    public float GoalProgress01 => Mathf.Clamp01(CurrentCleaningPercentage / goalPercentage);

    /// <summary>Se dispara cada vez que se registra un contaminante.</summary>
    public event Action<ContaminantDataSO> ContaminantRegistered;

    /// <summary>Se dispara una sola vez, al alcanzar la meta elegida.</summary>
    public event Action GoalReached;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        CleaningGoalSO goal = GameSession.SelectedGoal != null ? GameSession.SelectedGoal : defaultGoal;

        if (goal != null)
        {
            goalPercentage = Mathf.Max(1f, goal.targetPercentage);
        }
    }

    private void Start()
    {
        RecalculateTotal();
    }

    /// <summary>
    /// Suma el valor de todos los contaminantes de la escena (activos o no).
    /// Llamar de nuevo si se agregan contaminantes por código después del inicio.
    /// </summary>
    public void RecalculateTotal()
    {
        Contaminant[] all = FindObjectsByType<Contaminant>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        totalValue = 0f;

        foreach (Contaminant contaminant in all)
        {
            if (contaminant.Data != null)
            {
                totalValue += contaminant.Data.cleaningValue;
            }
        }

        if (totalValue <= 0f)
        {
            Debug.LogError("[CleaningManager] No se encontraron contaminantes con valor en la escena.");
        }
        else
        {
            Debug.Log($"[CleaningManager] Total de la escena (100%): {totalValue} | Meta: {goalPercentage}%");
        }
    }

    public void RegisterContaminant(ContaminantDataSO contaminantData)
    {
        if (goalReached) return;

        collectedValue += contaminantData.cleaningValue;

        Debug.Log(
            $"[CleaningManager] Recogido: {contaminantData.contaminantName} | " +
            $"Limpieza actual: {CurrentCleaningPercentage:F1}% | Meta: {goalPercentage}%");

        ContaminantRegistered?.Invoke(contaminantData);

        if (CurrentCleaningPercentage >= goalPercentage - GoalTolerance)
        {
            goalReached = true;
            GoalReached?.Invoke();
        }
    }
}