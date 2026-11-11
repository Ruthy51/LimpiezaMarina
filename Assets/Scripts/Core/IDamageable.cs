/**************************
 * Proyecto: LimpiezaMarina
 * Universidad de la Ciudad de Buenos Aires
 * Periodo: 2do cuatrimestre 2026
 * Materia: Programación 2
 * Integrantes: Ariana Ruiz, Ruth Wulf
 * Función: Interfaz de contrato para objetos dañables o purificables.
 * Explicación: Establece el método TakeDamage para desacoplar el emisor
 *              del ataque de la entidad receptora. Utiliza float para
 *              soportar daño instantáneo y daño continuo por exposición.
 *********************************************************************************************/

public interface IDamageable
{
    void TakeDamage(float amount);
}