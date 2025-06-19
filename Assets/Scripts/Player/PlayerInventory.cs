using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private ItemDataBase itemDataBase;
    [SerializeField] private InventoryManager inventoryUI;
    [SerializeField] private int maxSlots;
    private List<InventorySlot> slots;
    private PlayerController playerController;

    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        playerController = GetComponent<PlayerController>();
        //DontDestroyOnLoad(gameObject);
    }

    public List<InventorySlot> GetInventory()
    {
        return slots;
    }

    public void InitializeInventory()
    {
        slots = new List<InventorySlot>(maxSlots);
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot(null,i,0));
        }
        //inventoryUI.InitializeSlots();
    }

    public void LoadInventoryItems(List<SlotData> slotDataList)
    {
        foreach(SlotData slot in slotDataList)
        {
            slots[slot.slotIndex].quantity = slot.itemQuantity;
            slots[slot.slotIndex].item = itemDataBase.GetItemById(slot.itemId);
            //inventoryUI.AddItem(slots[slot.slotIndex].item, slot.slotIndex, slot.itemQuantity);
        }
    }

    public void OpenInventory()
    {
        //inventoryUI.ActiveInventory();
    }

    public void AddItem(Item item)
    {
        if (item.stack > 1)
        {
            InventorySlot existingSlot = slots.Find(slot => slot.item == item && slot.quantity < item.stack);
            if (existingSlot != null && existingSlot.quantity < item.stack)
            {
                existingSlot.quantity++;
                //inventoryUI.ChangeSlotQuantity(existingSlot.quantity, slots.IndexOf(existingSlot));
                return; //We added quantity to an existing item on the inventory
            }
        }

        InventorySlot emptySlot = slots.Find(slot => slot.item == null);
        if (emptySlot == null)
        {
            return; //There is no more space in the inventory
        }
        //We add an item in an empty slot
        emptySlot.item = item;
        emptySlot.quantity = 1;
        //inventoryUI.AddItem(item, slots.IndexOf(emptySlot), 1);
    }

    public void SwapOrStackItem(Item item, int quantity ,int index, int sourceSlotIndex)
    {
        InventorySlot targetSlot = slots[index];
        InventorySlot sourceslot = slots[sourceSlotIndex];
        if (targetSlot.item == item && targetSlot.quantity < item.stack)
        {
            int total = targetSlot.quantity + quantity;
            int rest = total - item.stack;
            if (rest > 0) // if there is rest, we leave the sourceSlot with that and the targetSlot with total-rest
            {
                sourceslot.quantity = rest; 
                //inventoryUI.ChangeSlotQuantity(sourceslot.quantity, sourceSlotIndex);
                targetSlot.quantity = total - rest;
            }
            else
            {
                targetSlot.quantity = total; 
                ClearSlot(sourceslot, sourceSlotIndex);
            }

            //inventoryUI.ChangeSlotQuantity(targetSlot.quantity, index);
            return;
        }
        else if (targetSlot.item != null)
        {
            //Item Swap
            sourceslot.quantity = targetSlot.quantity;
            sourceslot.item = targetSlot.item;
            //inventoryUI.AddItem(sourceslot.item, sourceSlotIndex, sourceslot.quantity);
            targetSlot.quantity = quantity;
            targetSlot.item = item;
            //inventoryUI.AddItem(targetSlot.item, index, targetSlot.quantity); 
        }
        else
        {
            //Item Change Slot
            targetSlot.quantity = quantity;
            targetSlot.item = item;
            ClearSlot(sourceslot, sourceSlotIndex);
            //inventoryUI.AddItem(item, index, quantity); 
        }
        AudioManager.Instance.PlayUI();
    }

    public void RemoveItem(Item item, int index)
    {
        InventorySlot slot = slots[index];
        if (slot != null)
        {
            slot.quantity--;
            //inventoryUI.ChangeSlotQuantity(slot.quantity, index);

            if (slot.quantity <= 0)
            {
                ClearSlot(slot, index);
            }
        }
        AudioManager.Instance.PlayUI();
    }

    public void ClearSlot(InventorySlot slot, int index)
    {
        slot.item = null;
        slot.quantity = 0;
        //inventoryUI.DeleteSlot(index);
    }

    public void UseItem(Item item, int index)
    {
        item.Use(playerController);
        AudioManager.Instance.PlayUseItem();
        RemoveItem(item, index);
    }
}
