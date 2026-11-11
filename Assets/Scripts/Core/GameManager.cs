/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestor general del juego.
 * Explicación: Controla la música ambiental, la carga de escenas
 * y las acciones principales del menú.
 *********************************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource musicaAmbiental;

    private void Start()
    {
        if (musicaAmbiental != null &&
            !musicaAmbiental.isPlaying)
        {
        //    Debug.Log("MUSICA START");
            musicaAmbiental.Play();
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Acuatico");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}