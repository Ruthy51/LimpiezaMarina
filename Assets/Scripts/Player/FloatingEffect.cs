/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026 
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Control del efecto visual de flotación oscilatoria.
 * Explicación: Aplica un movimiento suave sinusoidal en el eje Y local del objeto para simular
 * que el Aqua-Drone está flotando dinámicamente sobre la superficie del agua.
 *********************************************************************************************/

using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [Header("Parámetros de Flotación")]
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float amplitude = 0.15f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}