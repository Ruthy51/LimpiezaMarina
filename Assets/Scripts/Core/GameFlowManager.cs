/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestión del flujo general de la partida.
 * Explicación: Controla la visualización de paneles auxiliares,
 *              la navegación entre escenas, la secuencia inicial
 *              de presentación del lago y otras funciones globales
 *              asociadas al desarrollo de la partida.
 *********************************************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject panelControles;

    [Header("Cámaras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera introCamera;

    [Header("Jugador")]
    [SerializeField] private PlayerController3D playerController;

    [Header("Intro")]
    [SerializeField] private float introDuration = 3f;

    private bool controlesAbiertos;

    private void Start()
    {
        if (panelControles != null)
        {
            panelControles.SetActive(false);
        }

        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        if (introCamera != null)
        {
            introCamera.gameObject.SetActive(true);
        }

        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        yield return new WaitForSeconds(introDuration);

        if (introCamera != null)
        {
            introCamera.gameObject.SetActive(false);
        }

        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }

        if (playerController != null)
        {
            playerController.enabled = true;
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