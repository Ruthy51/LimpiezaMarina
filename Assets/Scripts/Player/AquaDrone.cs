/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Control de salud y recepción de daño para el AquaDrone del jugador.
 * Explicación: Implementa la interfaz IDamageable para procesar la reducción
 *              de energía del dron al entrar en contacto con ataques enemigos
 *              o fuentes de contaminación.
 *********************************************************************************************/

using System.Collections;
using UnityEngine;

public class AquaDrone : MonoBehaviour, IDamageable
{
    [Header("Configuración de Energía")]
    [SerializeField] private float maxHealth = 100f;

    [Header("Reacción al Impacto")]
    [SerializeField] private float squashAmount = 0.70f;
    [SerializeField] private float reactionDuration = 0.10f;

    private float currentHealth;

    private bool isReacting = false;
    private Vector3 originalScale;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        originalScale = transform.localScale;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        Debug.Log(
            $"[AquaDrone] Daño recibido: {amount}. Energía restante: {currentHealth}");

        if (currentHealth <= 0f)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Llamado desde EnemyMeleeAttack mediante Animation Event.
    /// </summary>
    public void OnEnemyImpact()
    {
        if (!isReacting)
        {
            StartCoroutine(HitReaction());
        }
    }

    private IEnumerator HitReaction()
    {
        isReacting = true;

        Vector3 squashScale =
            new Vector3(
                originalScale.x * 1.35f,
                originalScale.y * squashAmount,
                originalScale.z * 1.35f);

        float timer = 0f;

        // Se aplasta rápidamente
        while (timer < reactionDuration)
        {
            timer += Time.deltaTime;

            transform.localScale =
                Vector3.Lerp(
                    originalScale,
                    squashScale,
                    timer / reactionDuration);

            yield return null;
        }

        timer = 0f;

        // Recupera lentamente la forma
        while (timer < reactionDuration * 3f)
        {
            timer += Time.deltaTime;

            transform.localScale =
                Vector3.Lerp(
                    squashScale,
                    originalScale,
                    timer / (reactionDuration * 3f));

            yield return null;
        }

        transform.localScale = originalScale;

        isReacting = false;
    }

    private void GameOver()
    {
        Debug.Log(
            "[AquaDrone] El dron ha quedado inoperativo.");

        // TODO:
        // GameManager.Instance.GameOver();
    }
}