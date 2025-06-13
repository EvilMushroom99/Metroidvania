using UnityEngine;

[CreateAssetMenu(fileName = "NewJumpItem", menuName = "Inventory/JumpItem")]
public class JumpItem : Item
{
    public int jumpAmount;

    public override void Use(PlayerController player)
    {
        base.Use(player);
        player.jumpForce += jumpAmount;
        Debug.Log("Increasing JumpForce + " + jumpAmount);
    }
}
