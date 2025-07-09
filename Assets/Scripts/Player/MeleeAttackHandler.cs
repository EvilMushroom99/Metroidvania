using UnityEngine;

public class MeleeAttackHandler : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;

    public void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D target in hitEnemies)
        {
            target.GetComponent<IDamageable>()?.TakeDamage(attackDamage);
        }
    }
}
