/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Controla el panel de configuración que se abre y se cierra con la ruedita.
 * Explicación: Muestra y oculta el panel con un fundido (CanvasGroup), pausa y reanuda el juego,
 *              permite cerrar con Escape, oculta el botón Menú cuando ya se está en la escena de
 *              inicio y resuelve el regreso al menú principal.
 *********************************************************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuConfigUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private CanvasGroup panelConfig;
    [Tooltip("Botón Menú del panel. Se oculta cuando ya estamos en la escena de inicio.")]
    [SerializeField] private GameObject botonMenu;

    [Header("Ajustes")]
    [SerializeField] private string escenaMenu = "Inicio";
    [SerializeField] private float duracionFade = 0.2f;
    [SerializeField] private bool pausarJuego = true;

    private bool abierto;
    private float escalaTiempoPrevia = 1f;
    private Coroutine fadeActual;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += AlCargarEscena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AlCargarEscena;
    }

    private void Start()
    {
        FijarEstadoPanel(false);
        ActualizarBotonMenu(SceneManager.GetActiveScene());
    }

#if ENABLE_LEGACY_INPUT_MANAGER
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Alternar();
        }
    }
#endif

    // Conectar al On Click () de la ruedita.
    public void Alternar()
    {
        if (abierto) Cerrar();
        else Abrir();
    }

    // Conectar al On Click () del botón Menú.
    public void IrAlMenu()
    {
        CerrarInmediato();
        SceneManager.LoadScene(escenaMenu);
    }

    private void Abrir()
    {
        abierto = true;

        if (pausarJuego)
        {
            escalaTiempoPrevia = Time.timeScale;
            Time.timeScale = 0f;
        }

        panelConfig.interactable = true;
        panelConfig.blocksRaycasts = true;
        IniciarFade(1f);
    }

    private void Cerrar()
    {
        abierto = false;
        RestaurarTiempo();

        panelConfig.interactable = false;
        panelConfig.blocksRaycasts = false;
        IniciarFade(0f);
    }

    private void CerrarInmediato()
    {
        if (!abierto) return;

        abierto = false;
        RestaurarTiempo();

        if (fadeActual != null) StopCoroutine(fadeActual);
        FijarEstadoPanel(false);
    }

    private void RestaurarTiempo()
    {
        if (pausarJuego)
        {
            Time.timeScale = escalaTiempoPrevia;
        }
    }

    private void IniciarFade(float destino)
    {
        if (fadeActual != null) StopCoroutine(fadeActual);
        fadeActual = StartCoroutine(Fade(destino));
    }

    // Usa unscaledDeltaTime porque con el juego pausado (timeScale = 0) el fundido no avanzaría.
    private IEnumerator Fade(float destino)
    {
        float inicio = panelConfig.alpha;
        float t = 0f;

        while (t < duracionFade)
        {
            t += Time.unscaledDeltaTime;
            panelConfig.alpha = Mathf.Lerp(inicio, destino, t / duracionFade);
            yield return null;
        }

        panelConfig.alpha = destino;
        fadeActual = null;
    }

    private void FijarEstadoPanel(bool visible)
    {
        panelConfig.alpha = visible ? 1f : 0f;
        panelConfig.interactable = visible;
        panelConfig.blocksRaycasts = visible;
    }

    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        CerrarInmediato();
        ActualizarBotonMenu(escena);
    }

    private void ActualizarBotonMenu(Scene escena)
    {
        if (botonMenu != null)
        {
            botonMenu.SetActive(escena.name != escenaMenu);
        }
    }
}