using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Items/Usable")]
public class UsableItemSO : Item
{
    public List<ItemEffectSO> effects;

    public override void Use(GameObject user)
    {
        foreach (var effect in effects)
        {
            effect.Apply(user);
        }
    }
}
