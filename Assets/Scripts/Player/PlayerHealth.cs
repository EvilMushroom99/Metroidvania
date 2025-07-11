using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void TakeDamage(int damage)
    {
        stats.RestBaseValue(StatType.Health, damage);
    }
}