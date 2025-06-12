using UnityEngine;

[CreateAssetMenu(fileName = "NewHealingItem", menuName = "Inventory/HealingItem")]
public class HealingItem : Item
{
    public int healAmount;

    public override void Use()
    {
        base.Use();
        Debug.Log("Curando " + healAmount + " puntos de vida");
    }
}
