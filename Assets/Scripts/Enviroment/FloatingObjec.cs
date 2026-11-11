/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026 
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Simulación de flotación oscilatoria para elementos del escenario.
 * Explicación: Aplica un movimiento senoidal sobre el eje Y y una leve inclinación
 * en rotación para simular el bamboleo de objetos flotantes sobre el agua.
 *********************************************************************************************/

using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("Parámetros de Oscilación Vertical")]
    [SerializeField] private float floatAmplitude = 0.05f;
    [SerializeField] private float floatFrequency = 1.5f;

    [Header("Parámetros de Balanceo")]
    [SerializeField] private float tiltAmplitude = 3.0f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float timeOffset;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        
        // Offset aleatorio para desincronizar la flotación entre objetos
        timeOffset = Random.Range(0f, 10f);
    }

    private void Update()
    {
        float time = Time.time * floatFrequency + timeOffset;

        // Movimiento vertical en Y
        float newY = startPosition.y + Mathf.Sin(time) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Balanceo leve en X y Z
        float tiltX = Mathf.Sin(time * 0.8f) * tiltAmplitude;
        float tiltZ = Mathf.Cos(time * 0.6f) * tiltAmplitude;
        transform.rotation = startRotation * Quaternion.Euler(tiltX, 0f, tiltZ);
    }
}