using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform inventoryParent;

    private List<ItemUI> items = new();

    private void Awake()
    {
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < inventoryParent.childCount; i++)
        {
            ItemUI itemUI = inventoryParent.GetChild(i).GetComponentInChildren<ItemUI>();
            itemUI.InitializeItemUI(i);
            items.Add(itemUI);
        }
    }

    public void OpenOrCloseInventory()
    {
        if (inventoryPanel.activeInHierarchy) 
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].ForceEndDrag();
            }
            inventoryPanel.SetActive(false);
        }
        else inventoryPanel.SetActive(true);
    }

    public void RefreshInventoryUI()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].Refresh();
        }
    }

    //OLD
    /*
    public void AddItem(Item item, int index, int quantity)
    {
        InventorySlotUI slot = inventoryParent.GetChild(index).GetComponent<InventorySlotUI>();
        slot.InitializeSlot(item, quantity);
    }

    public void ChangeSlotQuantity(int quantity, int index)
    {
        inventoryParent.GetChild(index).GetComponent<InventorySlotUI>().UpdateSlot(quantity);
    }

    public void DeleteSlot(int index)
    {
        inventoryParent.GetChild(index).GetComponent<InventorySlotUI>().ClearSlot();
    }

    public void ActiveInventory()
    {
        if (inventoryPanel.activeInHierarchy)
        {
            for (int i = 0; i < inventoryParent.childCount; i++)
            {
                inventoryParent.GetChild(i).GetComponent<InventorySlotUI>().EndDrag();
            }
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
        }
    }
    */
}
