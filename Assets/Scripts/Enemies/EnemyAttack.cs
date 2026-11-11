/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Clase base abstracta que define la estructura y contrato común para los ataques.
 * Explicación: Centraliza el manejo de daño, cooldown, Animator y control temporal
 *              de ataques. Obliga a las clases derivadas a implementar su comportamiento
 *              específico mediante ExecuteAttack().
 *********************************************************************************************/

using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [Header("Configuración Base de Ataque")]
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float cooldown = 2f;
    [SerializeField] protected Animator animator;
    [SerializeField] protected string animationTriggerName;

    protected float lastAttackTime;

    public virtual bool CanAttack()
    {
        return Time.time >= lastAttackTime + cooldown;
    }

    protected void RegisterAttack()
    {
        lastAttackTime = Time.time;
    }

    public abstract void ExecuteAttack();
}