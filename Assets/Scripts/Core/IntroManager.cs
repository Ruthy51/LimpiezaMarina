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
using System.Collections;

public class IntroManager : MonoBehaviour
{
    [Header("Referencias del Momento 1 (Intro)")]
    [Tooltip("El cartel de la historia o subtítulos")]
    [SerializeField] private GameObject cartelHistoria;
    
    [Tooltip("El componente AudioSource que reproduce la voz de la historia ('lumi intro')")]
    [SerializeField] private AudioSource audioIntroduccion;

    [Header("Referencias del Momento 2 (Menú Interactivo)")]
    [Tooltip("El botón Jugar")]
    [SerializeField] private GameObject botonJugar;

    [Tooltip("El componente o panel para Elegir Tipo de Ecologista")]
    [SerializeField] private GameObject slotElegirTipoEcologista;

    [Tooltip("El componente o panel de Objetivos")]
    [SerializeField] private GameObject slotObjetivos;

    [Header("Configuración de Lumi")]
    [SerializeField] private Animator animatorLumi;
    [Tooltip("El Transform de Lumi para moverla a la posición del trigger al reingresar")]
    [SerializeField] private Transform transformLumi;

    private void Start()
    {
        // Verificamos si la intro ya se vio en esta ejecución del juego
        if (GameSettings.IntroYaVista)
        {
            // SI YA SE VIO (volviendo de Acuático, Victoria, Derrota, etc.):
            // Posicionamos a Lumi en el trigger y vamos directo al Momento 2
            IrAlMomento2Directamente();
        }
        else
        {
            // ES LA PRIMERA VEZ QUE SE ABRE EL JUEGO:
            // Ejecutamos el Momento 1 completo.
            StartCoroutine(SecuenciaMomento1());
        }
    }

    /// <summary>
    /// Momento 1: Cartel visible, botones y slots ocultos, audio sonando y Lumi caminando.
    /// </summary>
    private IEnumerator SecuenciaMomento1()
    {
        // 1. Mostrar cartel y OCULTAR el botón jugar y los dos slots
        if (cartelHistoria != null) cartelHistoria.SetActive(true);
        if (botonJugar != null) botonJugar.SetActive(false);
        if (slotElegirTipoEcologista != null) slotElegirTipoEcologista.SetActive(false);
        if (slotObjetivos != null) slotObjetivos.SetActive(false);

        // Activar animación de caminar en Lumi
        if (animatorLumi != null)
            animatorLumi.SetBool("Caminando", true);

        // 2. Reproducir el audio de la intro
        if (audioIntroduccion != null)
        {
            audioIntroduccion.Play();

            // Esperar a que termine de hablar por completo
            yield return new WaitWhile(() => audioIntroduccion.isPlaying);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }

        // 3. Cuando termina el audio, Lumi se detiene
        if (animatorLumi != null)
            animatorLumi.SetBool("Caminando", false);

        // 4. Marcamos que la intro ya se vio
        GameSettings.IntroYaVista = true;

        // 5. Activamos el Momento 2 (oculta cartel, muestra Jugar, Tipo de Ecologista y Objetivos a la vez)
        ActivarMomento2Visualmente();
    }

    /// <summary>
    /// Momento 2: Ocultar cartel, activar simultáneamente el botón Jugar y los dos slots.
    /// </summary>
    private void ActivarMomento2Visualmente()
    {
        // Ocultar el cartel de la historia
        if (cartelHistoria != null)
            cartelHistoria.SetActive(false);

        // Activar al mismo tiempo el botón Jugar y los dos paneles/slots
        if (botonJugar != null)
            botonJugar.SetActive(true);

        if (slotElegirTipoEcologista != null)
            slotElegirTipoEcologista.SetActive(true);

        if (slotObjetivos != null)
            slotObjetivos.SetActive(true);
    }

    /// <summary>
    /// Se ejecuta si ya se vio la intro (viniendo de otra escena): reposiciona a Lumi y salta al Momento 2.
    /// </summary>
    private void IrAlMomento2Directamente()
    {
        // Posicionar a Lumi en el trigger (x = 85, z = 74) manteniendo su altura (Y) actual
        if (transformLumi != null)
        {
            Vector3 posicionTrigger = new Vector3(85f, transformLumi.position.y, 74f);
            transformLumi.position = posicionTrigger;
        }

        if (animatorLumi != null)
            animatorLumi.SetBool("Caminando", false);

        // Activamos directamente el Momento 2 sin reproducir audio ni mostrar cartel
        ActivarMomento2Visualmente();
    }
}