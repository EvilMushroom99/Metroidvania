using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/Buff")]
public class StatBuffEffectSO : ItemEffectSO
{
    public StatType stat;
    public int amount;
    public float duration;

    public override void Apply(GameObject user)
    {
        if (user.TryGetComponent<CharacterStats>(out var stats))
        {
            stats.AddModifier(stat, amount);

            if (user.TryGetComponent<BuffHandler>(out var handler))
            {
                handler.StartBuffCoroutine(stat, amount, duration);
            }
        }
    }
}
