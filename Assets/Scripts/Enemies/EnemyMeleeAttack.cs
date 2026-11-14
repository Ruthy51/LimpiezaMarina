/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Control del ataque cuerpo a cuerpo (Melee) e interfaz de depuración visual.
 * Explicación: Hereda de EnemyAttack y aplica daño directo sobre objetivos que
 *              implementen la interfaz IDamageable. Incluye mensajes temporales
 *              de depuración para verificar impactos.
 *********************************************************************************************/

using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    [Header("Sonidos")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip impactSound;

    [Header("Depuración")]
    [SerializeField] private float bannerDuration = 1.5f;

    private bool showBanner = false;
    private string bannerMessage = "";

    private GameObject currentTarget;

    public override void ExecuteAttack()
    {
        // No se utiliza directamente.
    }

    public void ExecuteMeleeAttack(GameObject target)
    {
        if (!CanAttack())
            return;

        Debug.Log(
            $"[MELEE START] Objetivo: {target.name} | " +
            $"Distancia inicio: {Vector3.Distance(transform.position, target.transform.position):F2} | " +
            $"Frame: {Time.frameCount}");

        RegisterAttack();

        currentTarget = target;

        if (animator != null)
        {
            Debug.Log(
                $"[MELEE START] Lanzando trigger TackleAttack | Frame: {Time.frameCount}");

            animator.SetTrigger("TackleAttack");
        }
    }

    /// <summary>
    /// Animation Event ubicado en el frame de impacto.
    /// </summary>
    public void OnMeleeImpactFrame()
    {
        Debug.Log(
            $"[MELEE IMPACT] Frame de impacto recibido | Frame: {Time.frameCount}");

        if (currentTarget != null)
        {
            float distance =
                Vector3.Distance(
                    transform.position,
                    currentTarget.transform.position);

            Debug.Log(
                $"[MELEE IMPACT] Target: {currentTarget.name} | " +
                $"Distancia actual: {distance:F2}");
        }
        else
        {
            Debug.LogWarning(
                "[MELEE IMPACT] currentTarget es NULL");
        }

        if (audioSource != null &&
            impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }

        if (currentTarget != null &&
            currentTarget.TryGetComponent<IDamageable>(
                out IDamageable damageable))
        {
            Debug.Log(
                $"[MELEE IMPACT] Aplicando daño: {damage}");

            damageable.TakeDamage(damage);

            currentTarget.SendMessage(
                "OnEnemyImpact",
                SendMessageOptions.DontRequireReceiver);

          //  StartCoroutine(
          //      DisplayBanner(
          //          $"¡ATAQUE MELEE IMPACTÓ A {currentTarget.name.ToUpper()}!"));
        }
        else
        {
            Debug.LogWarning(
                "[MELEE IMPACT] No se encontró un objetivo válido para dañar.");

         //   StartCoroutine(
         //       DisplayBanner(
         //           "¡ATAQUE MELEE EJECUTADO (SIN BLANCO)!"));
        }
    }

    private IEnumerator DisplayBanner(string message)
    {
        bannerMessage = message;
        showBanner = true;

        Debug.Log(
            $"<color=red><b>[MELEE]</b> {message}</color>");

        yield return new WaitForSeconds(bannerDuration);

        showBanner = false;
    }

    private void OnGUI()
    {
        if (!showBanner)
            return;

        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);

        boxStyle.fontSize = 20;
        boxStyle.fontStyle = FontStyle.Bold;
        boxStyle.normal.textColor = Color.yellow;
        boxStyle.alignment = TextAnchor.MiddleCenter;

        GUI.Box(
            new Rect(Screen.width / 2 - 250, 40, 500, 50),
            bannerMessage,
            boxStyle);
    }
}