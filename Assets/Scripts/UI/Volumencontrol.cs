/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Conecta el slider de volumen del menú con el audio general del juego.
 * Explicación: Al iniciar carga el volumen guardado en PlayerPrefs y lo aplica a AudioListener.
 *              Cada vez que el jugador mueve el slider actualiza el volumen y lo guarda; al soltar
 *              el slider fuerza el guardado para que también persista en WebGL.
 *********************************************************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumenControl : MonoBehaviour, IPointerUpHandler
{
    private const string ClaveVolumen = "VolumenGeneral";

    [SerializeField, Range(0f, 1f)] private float valorPorDefecto = 0.8f;

    private Slider slider;

   private void Awake()
{
   
    slider = GetComponent<Slider>();

  
    slider.minValue = 0f;
    slider.maxValue = 1f;

    float guardado =
        PlayerPrefs.GetFloat(
            ClaveVolumen,
            valorPorDefecto);

    slider.SetValueWithoutNotify(guardado);

    AplicarVolumen(guardado);

    slider.onValueChanged.AddListener(CambiarVolumen);

    // PRUEBA
    slider.onValueChanged.AddListener(
        (valor) =>
        {
          //  Debug.Log("EVENTO DIRECTO: " + valor);
        });
}
    private void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(CambiarVolumen);
        }
    }
public void Prueba()
{
   // Debug.Log("PRUEBA");
}
    private void CambiarVolumen(float valor)
    {
 
        AplicarVolumen(valor);
        PlayerPrefs.SetFloat(ClaveVolumen, valor);
    }

    private static void AplicarVolumen(float valor)
    {
        AudioListener.volume = valor;
    }

    // Al soltar el slider se escribe en disco (en WebGL hace falta llamar a Save explícitamente).
    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerPrefs.Save();
    }
}