/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Gestión de escenas finales.
 * Explicación: Permite regresar al menú principal desde
 *              las escenas de Victoria y Derrota.
 *********************************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{
    [Header("Escenas")]
    [SerializeField] private string menuScene = "Inicio";

    public void IrAlMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}