/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestión del flujo general de la partida.
 * Explicación: Controla la visualización de paneles auxiliares,
 *              la navegación entre escenas y otras funciones
 *              globales asociadas al desarrollo de la partida.
 *********************************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject panelControles;

    private bool controlesAbiertos;

    private void Start()
    {
        if (panelControles != null)
        {
            panelControles.SetActive(false);
        }
    }

    /// <summary>
    /// Abre o cierra el panel de controles.
    /// </summary>
    public void ToggleControles()
    {
        if (panelControles == null)
        {
            Debug.LogWarning(
                "[GameFlowManager] No se asignó PanelControles.");
            return;
        }

        controlesAbiertos = !controlesAbiertos;

        panelControles.SetActive(controlesAbiertos);
    }

    /// <summary>
    /// Carga la escena de victoria.
    /// </summary>
    public void IrAVictoria()
    {
        SceneManager.LoadScene("Victoria");
    }

    /// <summary>
    /// Carga la escena de derrota.
    /// </summary>
    public void IrADerrota()
    {
        SceneManager.LoadScene("Derrota");
    }
}