/****
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Almacena configuraciones globales de la partida.
 * Explicación: Permite transferir entre escenas la meta de limpieza
 *              elegida por el jugador desde la pantalla de Inicio.
 ***********/

using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // Bandera para la introducción (lo nuevo que agregamos)
    public static bool IntroYaVista = false;

    // Variable que ya usaba tu juego para el porcentaje de objetivos
    public static float SelectedGoalPercentage = 100f; // o el valor por defecto que usaras
}