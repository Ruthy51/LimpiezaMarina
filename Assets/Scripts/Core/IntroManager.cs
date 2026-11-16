/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestión centralizada del progreso de limpieza ambiental.
 * Explicación: Calcula el valor total de los contaminantes presentes
 *              en la escena (equivalente al 100%), acumula lo recogido,
 *              aplica la meta elegida por el jugador y avisa cuando se
 *              alcanza para activar la victoria.
 *********************************************************************************************/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [Header("Referencias de Componentes")]
    public AudioSource audioSource;
    public TextMeshProUGUI textoSubtitulos;

    [Header("Paneles de UI a Ocultar/Mostrar")]
    public GameObject panelEcologistaUI; // El slider de "¿Qué tipo de Ecologista sos?"
    public GameObject panelBotonesMenu;  // Los botones JUGAR / SALIR (opcional si querés ocultarlos)

    [Header("Configuración de Tiempos")]
    public float duracionTotalIntro = 13.0f;
    public string nombreEscenaJuego = "Scene_Game";

    void Start()
    {
        // 1. Ocultamos el panel de ecologista/niveles durante la intro
        if (panelEcologistaUI != null)
            panelEcologistaUI.SetActive(false);

        // 2. Reproducimos el audio
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // 3. Programamos la aparición de cada frase por separado
        Invoke("MostrarLinea1", 0.0f);  // 0s
        Invoke("MostrarLinea2", 4.0f);  // 4s
        Invoke("MostrarLinea3", 8.5f);  // 8.5s

        // 4. Al terminar la intro, activamos la UI o cargamos la escena
        Invoke("FinalizarIntro", duracionTotalIntro);
    }

    void Update()
    {
        // Saltar la intro al tocar Espacio o Clic
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CancelInvoke();
            FinalizarIntro();
        }
    }

    void MostrarLinea1()
    {
        if (textoSubtitulos != null)
            textoSubtitulos.text = "Año 2088. Este lugar antes era hermoso...";
    }

    void MostrarLinea2()
    {
        if (textoSubtitulos != null)
            textoSubtitulos.text = "Pero la contaminación lo arruinó todo...";
    }

    void MostrarLinea3()
    {
        if (textoSubtitulos != null)
            textoSubtitulos.text = "Es hora de probar mi invento. ¡A limpiar!";
    }

    void FinalizarIntro()
    {
        // Borramos el texto de los subtítulos
        if (textoSubtitulos != null)
            textoSubtitulos.text = "";

        // Mostramos la interfaz de Ecologista / Selección de nivel
        if (panelEcologistaUI != null)
            panelEcologistaUI.SetActive(true);

        // Si prefieres que pase directo a la escena de la laguna:
        // SceneManager.LoadScene(nombreEscenaJuego);
    }
}