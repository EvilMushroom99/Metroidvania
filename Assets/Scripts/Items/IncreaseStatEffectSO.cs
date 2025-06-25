using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/IncreaseStat")]
public class IncreaseStatEffectSO : ItemEffectSO
{
    public StatType statType;
    public int amount;

    public override void Apply(GameObject user)
    {
        if (user.TryGetComponent<CharacterStats>(out var stats))
        {
            stats.AddModifier(statType, amount);
        }
    }
}
