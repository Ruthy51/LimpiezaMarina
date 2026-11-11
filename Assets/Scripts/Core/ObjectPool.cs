/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026 
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Pool de objetos genérico y reutilizable por componente.
 * Explicación: Reutiliza instancias de proyectiles y efectos en memoria evitando la
 * instanciación y destrucción constante, optimizando el recolector de basura (GC).
 * Al ser un componente independiente, permite disponer de múltiples pools en la escena.
 *********************************************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Configuración del Pool")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialPoolSize = 10;

    private List<GameObject> pooledObjects;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObject();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i] != null && !pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // Si se agotan los objetos iniciales, expande el pool dinámicamente
        return CreateNewObject();
    }

    private GameObject CreateNewObject()
    {
        if (prefab == null)
        {
            Debug.LogError($"[ObjectPool en {gameObject.name}] ¡El Prefab no está asignado en el Inspector!");
            return null;
        }

        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}