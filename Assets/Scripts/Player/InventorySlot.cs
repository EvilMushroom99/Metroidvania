public class InventorySlot
{
    public Item item;
    public int slotIndex;
    public int quantity;

    public InventorySlot(Item item,int index,int quantity)
    {
        this.item = item;
        this.slotIndex = index;
        this.quantity = quantity;
    }
}
