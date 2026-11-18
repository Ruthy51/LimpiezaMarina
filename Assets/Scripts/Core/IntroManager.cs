/**********
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestión de la introducción narrativa.
 * Explicación: Reproduce la introducción inicial una única vez.
 *              En regresos posteriores al menú se omite y se
 *              habilita directamente la interfaz principal.
 *******************************/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [Header("Referencias de Componentes")]
    public AudioSource audioSource;
    public TextMeshProUGUI textoSubtitulos;

    [Header("Paneles de UI a Ocultar/Mostrar")]
    public GameObject panelEcologistaUI;
    public GameObject panelBotonesMenu;

    [Header("Configuración de Tiempos")]
    public float duracionTotalIntro = 13.0f;
    public string nombreEscenaJuego = "Scene_Game";

    private void Start()
    {
        // Si ya vio la introducción una vez,
        // saltamos directamente al menú.
        if (GameSettings.IntroYaVista)
        {
            SaltarIntroduccion();
            return;
        }

        if (panelEcologistaUI != null)
            panelEcologistaUI.SetActive(false);

        if (audioSource != null)
        {
            audioSource.Play();
        }

        Invoke(nameof(MostrarLinea1), 0.0f);
        Invoke(nameof(MostrarLinea2), 4.0f);
        Invoke(nameof(MostrarLinea3), 8.5f);

        Invoke(nameof(FinalizarIntro), duracionTotalIntro);
    }

    private void Update()
    {
        // Saltar la intro con espacio o click
        if (!GameSettings.IntroYaVista &&
            (Input.GetKeyDown(KeyCode.Space) ||
             Input.GetMouseButtonDown(0)))
        {
            CancelInvoke();
            FinalizarIntro();
        }
    }

    private void MostrarLinea1()
    {
        if (textoSubtitulos != null)
            textoSubtitulos.text =
                "Año 2088. Este lugar antes era hermoso...";
    }

    private void MostrarLinea2()
    {
        if (textoSubtitulos != null)
            textoSubtitulos.text =
                "Pero la contaminación lo arruinó todo...";
    }

    private void MostrarLinea3()
    {
        if (textoSubtitulos != null)
            textoSubtitulos.text =
                "Es hora de probar mi invento. ¡A limpiar!";
    }

    private void FinalizarIntro()
    {
        if (textoSubtitulos != null)
            textoSubtitulos.text = "";

        if (audioSource != null)
            audioSource.Stop();

        if (panelEcologistaUI != null)
            panelEcologistaUI.SetActive(true);

        // La intro ya fue vista.
        GameSettings.IntroYaVista = true;
    }

    private void SaltarIntroduccion()
    {
        if (textoSubtitulos != null)
            textoSubtitulos.text = "";

        if (panelEcologistaUI != null)
            panelEcologistaUI.SetActive(true);
    }
}