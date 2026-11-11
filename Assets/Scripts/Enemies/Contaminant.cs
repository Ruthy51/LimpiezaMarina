/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Representación de un contaminante recolectable.
 * Explicación: Contiene los datos configurables del contaminante y
 *              notifica al gestor de limpieza cuando es recogido por
 *              el dron mediante el CleanerCone.
 *********************************************************************************************/

using UnityEngine;

public class Contaminant : MonoBehaviour
{
    [Header("Datos del Contaminante")]
    [SerializeField] private ContaminantDataSO contaminantData;

    public ContaminantDataSO Data => contaminantData;

    public void Collect()
    {
        if (contaminantData != null)
        {
            CleaningManager.Instance.RegisterContaminant(contaminantData);
        }

        gameObject.SetActive(false);
    }
}