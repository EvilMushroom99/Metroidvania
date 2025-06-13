using UnityEngine;

[CreateAssetMenu(fileName = "NewHealingItem", menuName = "Inventory/HealingItem")]
public class HealingItem : Item
{
    public int healAmount;

    public override void Use(PlayerController playerController)
    {
        base.Use(playerController);
        //player.health += healAmount
        Debug.Log("Healing " + healAmount + " HP");
    }
}
