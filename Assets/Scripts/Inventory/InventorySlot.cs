using System;

public class InventorySlot
{
    public Item item;
    public int slotIndex;
    public int quantity;

    public InventorySlot(Item item,int slotIndex, int quantity)
    {
        this.item = item;
        this.slotIndex = slotIndex;
        this.quantity = quantity;
    }
}
