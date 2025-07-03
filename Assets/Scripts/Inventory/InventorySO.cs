using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Systems/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private ItemDataBase itemDataBase;
    [SerializeField] private int maxSlots;

    private List<InventorySlot> slots;
    public GameEvent onInventoryChanged;

    public void InitializeInventory()
    {
        slots = new List<InventorySlot>(maxSlots);
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot(null, i, 0));
        }
    }

    public void LoadInventoryItems(List<SlotData> slotDataList)
    {
        foreach (SlotData slot in slotDataList) 
        {
            slots[slot.slotIndex].quantity = slot.itemQuantity;
            slots[slot.slotIndex].item = itemDataBase.GetItemById(slot.itemId);
        }
        onInventoryChanged.Raise();
    }

    public int GetSlotCount() 
    { 
        return maxSlots; 
    }

    public InventorySlot GetSlot(int index) 
    {
        if (index < 0 || index >= slots.Count)
        {
            Debug.LogWarning($"[InventorySO] Index out of range: {index}");
            return null;
        }
        return slots[index];
    }

    public List<InventorySlot> GetAllSlots()
    {
        if(slots.Count != 0) return new List<InventorySlot>(slots);
        else return new List<InventorySlot>();
    }

    public bool AddItem(Item item)
    {
        if (item.stack > 1)
        {
            InventorySlot existingSlot = slots.Find(slot => slot.item == item && slot.quantity < item.stack);
            if (existingSlot != null && existingSlot.quantity < item.stack)
            {
                existingSlot.quantity++;
                onInventoryChanged.Raise();
                return true; //We added quantity to an existing item on the inventory
            }
        }

        InventorySlot emptySlot = slots.Find(slot => slot.item == null);
        if (emptySlot == null)
        {
            return false; //There is no more space in the inventory
        }
        //We add an item in an empty slot
        AssignItemToSlot(emptySlot, item, 1);
        onInventoryChanged.Raise();
        return true;
    }
    public void SwapOrStackItem(Item item, int quantity, int index, int sourceSlotIndex)
    {
        InventorySlot targetSlot = slots[index];
        InventorySlot sourceslot = slots[sourceSlotIndex];
        if (targetSlot.item == item && targetSlot.quantity < item.stack)
        {
            //Item Stack
            int total = targetSlot.quantity + quantity;
            int rest = total - item.stack;
            if (rest > 0) // if there is rest, we leave the sourceSlot with that and the targetSlot with total-rest
            {
                sourceslot.quantity = rest;
                targetSlot.quantity = total - rest;
            }
            else
            {
                targetSlot.quantity = total;
                ClearSlot(sourceslot);
            }
        }
        else if (targetSlot.item != null)
        {
            //Item Swap
            AssignItemToSlot(sourceslot, targetSlot.item, targetSlot.quantity);
            AssignItemToSlot(targetSlot, item, quantity);
        }
        else
        {
            AssignItemToSlot(targetSlot, item, quantity);
            ClearSlot(sourceslot);
        }
        onInventoryChanged.Raise();
    }

    public void RemoveItem(Item item, int index)
    {
        InventorySlot slot = slots[index];
        if (slot.item == null || slot.item != item)
            return;

        slot.quantity--;

        if (slot.quantity <= 0)
        {
            ClearSlot(slot);
        }
        
        onInventoryChanged.Raise();
    }

    private void AssignItemToSlot(InventorySlot slot, Item item, int quantity)
    {
        slot.item = item;
        slot.quantity = quantity;
    }

    private void ClearSlot(InventorySlot slot)
    {
        slot.item = null;
        slot.quantity = 0;
    }

    public void UseItem(int index, GameObject user)
    {
        slots[index].item.Use(user);
        RemoveItem(slots[index].item, index);
    }
}
