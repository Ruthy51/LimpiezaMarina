/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestión centralizada del progreso de limpieza ambiental.
 * Explicación: Acumula el porcentaje de limpieza, registra los
 *              contaminantes recogidos y servirá como base para
 *              la condición de victoria y la actualización del HUD.
 *********************************************************************************************/

using UnityEngine;

public class CleaningManager : MonoBehaviour
{
    public static CleaningManager Instance { get; private set; }

    [Header("Progreso")]
    [SerializeField] private float currentCleaningPercentage;

    public float CurrentCleaningPercentage => currentCleaningPercentage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterContaminant(ContaminantDataSO contaminantData)
    {
        currentCleaningPercentage += contaminantData.cleaningValue;

        currentCleaningPercentage =
            Mathf.Clamp(currentCleaningPercentage, 0f, 100f);

        Debug.Log(
            $"[CleaningManager] Recogido: {contaminantData.contaminantName} | " +
            $"Limpieza actual: {currentCleaningPercentage}%");
    }
}