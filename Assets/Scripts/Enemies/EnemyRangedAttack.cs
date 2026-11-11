/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Control de ráfaga temporizada de partículas de petróleo para ataque a distancia.
 * Explicación: Hereda de EnemyAttack, dispara la animación de ataque ranged
 *              y utiliza un Animation Event para sincronizar la emisión real
 *              del petróleo con la animación.
 *********************************************************************************************/

using System.Collections;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttack
{
    [Header("Componentes de Ataque")]
    [SerializeField] private ParticleSystem petroleoParticleSystem;
    [SerializeField] private float streamDuration = 1.5f;

    private Coroutine streamCoroutine;

    private bool attackBlocked = false;

    private void Awake()
    {
        if (petroleoParticleSystem != null)
        {
            petroleoParticleSystem.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public override void ExecuteAttack()
    {
        if (attackBlocked)
            return;

        if (!CanAttack())
            return;

        RegisterAttack();

        if (animator != null)
        {
            animator.SetTrigger("RangedAttack");
        }

        Debug.Log(
            "[EnemyRangedAttack] Ataque Ranged iniciado. Cooldown en marcha.");
    }

    public void OnRangedAttackFrame()
    {
        if (attackBlocked)
            return;

        Debug.Log(
            "[EnemyRangedAttack] Animation Event recibido.");

        if (petroleoParticleSystem == null)
            return;

        if (streamCoroutine != null)
        {
            StopCoroutine(streamCoroutine);
        }

        streamCoroutine = StartCoroutine(
            EmitPetroleoBurst());
    }

    private IEnumerator EmitPetroleoBurst()
    {
        Debug.Log(
            "[EnemyRangedAttack] Emitiendo ráfaga de petróleo...");

        petroleoParticleSystem.Play();

        yield return new WaitForSeconds(streamDuration);

        petroleoParticleSystem.Stop(
            true,
            ParticleSystemStopBehavior.StopEmitting);

        Debug.Log(
            "[EnemyRangedAttack] Ráfaga finalizada.");
    }

    public void BlockRangedAttack()
    {
        attackBlocked = true;

        if (streamCoroutine != null)
        {
            StopCoroutine(streamCoroutine);
            streamCoroutine = null;
        }

        if (petroleoParticleSystem != null)
        {
            petroleoParticleSystem.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public void UnblockRangedAttack()
    {
        attackBlocked = false;
    }
}