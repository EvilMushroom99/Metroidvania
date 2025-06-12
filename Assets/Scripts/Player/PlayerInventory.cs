using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private int maxSlots;
    private List<InventorySlot> slots;

    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
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
            return;
        }
        emptySlot.item = item;
        emptySlot.quantity = 1;
        inventoryUI.AddItem(item, slots.IndexOf(emptySlot), 1);
    }

    public void InsertItem(Item item, int quantity ,int index, int originSlotIndex)
    {
        
        InventorySlot slot = slots[index];
        InventorySlot originslot = slots[originSlotIndex];
        if (slot.item == item && slot.quantity < item.stack)
        {
            int total = slot.quantity + quantity;
            int rest = total - item.stack;
            if (rest > 0)
            {
                originslot.quantity = rest;
                inventoryUI.ChangeSlotQuantity(originslot.quantity, originSlotIndex);
                slot.quantity = total - rest;
            }
            else
            {
                slot.quantity = total;
                ClearSlot(originslot, originSlotIndex);
            }

            Debug.Log("cantidad del slot: " + slot.quantity);
            inventoryUI.ChangeSlotQuantity(slot.quantity, index);
            return;
        }
        else if (slot.item != null)
        {
            originslot.quantity = slot.quantity;
            originslot.item = slot.item;
            inventoryUI.AddItem(originslot.item, originSlotIndex, originslot.quantity);
            slot.quantity = quantity;
            slot.item = item;
            inventoryUI.AddItem(slot.item, index, slot.quantity); 
            Debug.Log("Swap items");
        }
        else
        {
            Debug.Log("cantidad: " + quantity);

            slot.quantity = quantity;
            slot.item = item;
            ClearSlot(originslot, originSlotIndex);
            inventoryUI.AddItem(item, index, quantity); 
        }
    }

    public void RemoveItem(Item item, int index)
    {
        InventorySlot slot = slots[index];
        if (slot != null)
        {
            slot.quantity--;
            inventoryUI.ChangeSlotQuantity(slot.quantity, index);
            Debug.Log("Item Deleted");

            if (slot.quantity <= 0)
            {
                ClearSlot(slot, index);
            }
        }
    }

    public void ClearSlot(InventorySlot slot, int index)
    {
        slot.item = null;
        slot.quantity = 0;
        inventoryUI.DeleteSlot(index);
        Debug.Log("Slot eliminado");
    }

    public void UseItem(Item item, int index)
    {
        item.Use();
        RemoveItem(item, index);
    }
}
