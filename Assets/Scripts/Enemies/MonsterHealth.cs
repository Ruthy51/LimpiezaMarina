/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Control de vida y recepción de daño para el monstruo acuático.
 * Explicación: Implementa la interfaz IDamageable para recibir daño de las
 *              burbujas disparadas por el dron. Al llegar a cero puntos de
 *              vida ejecuta la lógica de muerte y elimina al enemigo.
 *********************************************************************************************/

using UnityEngine;

public class MonsterHealth : MonoBehaviour, IDamageable
{
    [Header("Configuración de Vida")]
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        Debug.Log(
            $"[MonsterHealth] Daño recibido: {amount}. Vida restante: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("[MonsterHealth] Monstruo derrotado.");

        gameObject.SetActive(false);
    }
}