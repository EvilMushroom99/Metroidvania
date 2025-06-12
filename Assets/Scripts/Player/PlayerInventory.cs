using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private int maxSlots;
    private List<InventorySlot> slots;


    private void Awake()
    {
        InitializeInventory();
    }

    public void InitializeInventory()
    {
        slots = new List<InventorySlot>(maxSlots);
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot(null,i,0));
        }
    }

    public void AddItem(Item item)
    {
        if (item.stack > 1)
        {
            InventorySlot existingSlot = slots.Find(slot => slot.item == item && slot.quantity < item.stack);
            if (existingSlot != null && existingSlot.quantity < item.stack)
            {
                existingSlot.quantity++;
                inventoryUI.ChangeSlotQuantity(existingSlot.quantity, slots.IndexOf(existingSlot));
                Debug.Log("Agregando item existente");
                return;
            }
            else
            {
                Debug.Log("No existe este item en el inventario");
            }
        }

        InventorySlot emptySlot = slots.Find(slot => slot.item == null);
        if (emptySlot == null)
        {
            Debug.Log("No hay espacio en el inventario");
            return;
        }
        emptySlot.item = item;
        emptySlot.quantity = 1;
        inventoryUI.AddItem(item, slots.IndexOf(emptySlot));
        Debug.Log("Agregando nuevo item");
    }

    public void RemoveItem(Item item, int index)
    {
        InventorySlot slot = slots[index];
        if (slot != null)
        {
            slot.quantity--;
            inventoryUI.ChangeSlotQuantity(slot.quantity, index);
            Debug.Log("Item Eliminado eliminado");

            if (slot.quantity <= 0)
            {
                slot.item = null;
                inventoryUI.DeleteSlot(index);
                Debug.Log("Slot eliminado");
            }
        }
    }

    public void UseItem(Item item, int index)
    {
        item.Use();
        RemoveItem(item, index);
    }
}
