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
using TMPro;
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
    [SerializeField] private float introDuration = 6f;

    [Header("Texto Intro")]
    [SerializeField] private TMP_Text objetivoIntroText;

    [Header("Audio Intro")]
    [SerializeField] private AudioSource introAudioSource;
    [SerializeField] private AudioClip introAmbientClip;

    private bool controlesAbiertos;

    private void Start()
    {
        if (panelControles != null)
        {
            panelControles.SetActive(false);
        }

        if (CleaningManager.Instance != null)
        {
            CleaningManager.Instance.GoalReached += IrAVictoria;
        }

        StartCoroutine(PlayIntro());
    }

    private void OnDestroy()
    {
        if (CleaningManager.Instance != null)
        {
            CleaningManager.Instance.GoalReached -= IrAVictoria;
        }
    }

    private IEnumerator PlayIntro()
    {
        if (objetivoIntroText != null)
        {
            objetivoIntroText.text =
                $"OBJETIVO DE LIMPIEZA\n\n{Mathf.RoundToInt(GameSettings.SelectedGoalPercentage)}%";

            objetivoIntroText.gameObject.SetActive(true);
        }

        if (introAudioSource != null &&
            introAmbientClip != null)
        {
            Debug.Log("[INTRO] Reproduciendo ambiente.");

            introAudioSource.clip = introAmbientClip;
            introAudioSource.Play();
        }

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

        if (introAudioSource != null &&
            introAudioSource.isPlaying)
        {
            introAudioSource.Stop();
        }

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

        if (objetivoIntroText != null)
        {
            objetivoIntroText.gameObject.SetActive(false);
        }
    }

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

    public void IrAVictoria()
    {
        Debug.Log(
            "[GameFlowManager] Cargando escena Victoria.");

        SceneManager.LoadScene("Victoria");
    }

    public void IrADerrota()
    {
        Debug.Log(
            "[GameFlowManager] Cargando escena Derrota.");

        SceneManager.LoadScene("Derrota");
    }
}