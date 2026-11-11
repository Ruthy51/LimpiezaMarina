/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Seguimiento del AquaDrone mediante una cámara Top View.
 * Explicación: Mantiene la cámara centrada sobre el jugador utilizando
 *              un desplazamiento configurable (offset). Está diseñado
 *              para escenarios grandes donde el jugador necesita una
 *              referencia visual constante durante la exploración.
 *********************************************************************************************/

using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform target;

    [Header("Configuración de Cámara")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 18f, -8f);

    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = target.position + offset;
    }
}