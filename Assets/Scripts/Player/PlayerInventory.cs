using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private int maxSlots;
    private List<InventorySlot> slots = new();

    public void AddItem(Item item)
    {
        if (item.stack > 1)
        {
            InventorySlot existingSlot = slots.Find(slot => slot.item == item);
            if (existingSlot != null)
            {
                existingSlot.quantity++;
                inventoryUI.ChangeSlotQuantity(existingSlot.quantity, slots.IndexOf(existingSlot));
                Debug.Log("Agregando item existente");
            }
            else if (slots.Count >= maxSlots)
            {
                Debug.Log("No hay espacio en el inventario");
            }
            else
            {
                InventorySlot newSlot = new(item, slots.Count, 1);
                slots.Add(newSlot);
                inventoryUI.AddItem(item);
                Debug.Log("Agregando nuevo item");
            }
        }
    }

    public void RemoveItem(Item item)
    {
        InventorySlot slot = slots.Find(s => s.item == item);
        if (slot != null)
        {
            slot.quantity--;
            inventoryUI.ChangeSlotQuantity(slot.quantity, slots.IndexOf(slot));
            Debug.Log("Item Eliminado eliminado");

            if (slot.quantity <= 0)
            {
                slots.Remove(slot);
                Debug.Log("Slot eliminado");
            }
        }
    }
}
