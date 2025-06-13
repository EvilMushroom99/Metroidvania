using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedItem", menuName = "Inventory/SpeedItem")]
public class SpeedItem : Item
{
    public int speedAmount;

    public override void Use(PlayerController player)
    {
        base.Use(player);
        player.speedMultiplier += speedAmount;
        Debug.Log("Increasing Speed + " + speedAmount);
    }
}
