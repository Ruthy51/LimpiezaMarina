/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Control de navegación sobre el lago.
 * Explicación: Impide que el AquaDrone abandone el agua
 * comprobando la altura del terreno antes de permitir
 * cada desplazamiento.
 *********************************************************************************************/

using UnityEngine;

public class WaterNavigator : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Terrain terrain;

    [Header("Agua")]
    [SerializeField] private float waterY = 0f;

    [SerializeField] private float margin = 0.5f;

    public bool IsWater(Vector3 position)
    {
        float terrainHeight =
            terrain.SampleHeight(position)
            + terrain.transform.position.y;

        return terrainHeight < waterY - margin;
    }
}