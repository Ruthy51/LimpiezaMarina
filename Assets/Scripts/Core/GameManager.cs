/**********
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestor general del juego.
 * Explicación: Controla la música ambiental, la carga de escenas,
 * las acciones principales del menú y la selección del objetivo
 * de limpieza antes de comenzar la partida.
 *******************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource musicaAmbiental;

    [Header("Objetivo de Limpieza")]
    [SerializeField] private TMP_Dropdown objetivos;

    private void Start()
    {
        if (musicaAmbiental != null &&
            !musicaAmbiental.isPlaying)
        {
            musicaAmbiental.Play();
        }
    }

    public void Jugar()
    {
        if (objetivos != null)
        {
            switch (objetivos.value)
            {
                case 0:
                    GameSettings.SelectedGoalPercentage = 20f;
                    break;

                case 1:
                    GameSettings.SelectedGoalPercentage = 40f;
                    break;

                case 2:
                    GameSettings.SelectedGoalPercentage = 60f;
                    break;

                case 3:
                    GameSettings.SelectedGoalPercentage = 80f;
                    break;

                case 4:
                    GameSettings.SelectedGoalPercentage = 100f;
                    break;

                default:
                    GameSettings.SelectedGoalPercentage = 60f;
                    break;
            }

            Debug.Log(
                $"Meta seleccionada: {GameSettings.SelectedGoalPercentage}%");
        }

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