/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Detección y recolección de contaminantes mediante el cono extractor.
 * Explicación: Detecta contaminantes dentro de su área de influencia,
 *              registra su recolección y los elimina del escenario.
 *********************************************************************************************/

using UnityEngine;

public class CleanerCone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            return;
        }

        if (other.gameObject.name.Contains("WaterSurface"))
        {
            return;
        }

        if (other.TryGetComponent<Contaminant>(out Contaminant contaminant))
        {
            Debug.Log(
                $"[CleanerCone] Contaminante recolectado: {other.gameObject.name}");

            contaminant.Collect();
        }
    }
}