using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private RectTransform itemDragHandler;

    private List<ItemUI> items = new();

    private void Awake()
    {
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        int slotCount = inventory.GetSlotCount();
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryParent);
            ItemUI itemUI = slot.GetComponentInChildren<ItemUI>();
            itemUI.InitializeItemUI(this, i, itemDragHandler);
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

    public void RemoveItem(int slotIndex)
    {
        InventorySlot slot = inventory.GetSlot(slotIndex);
        if (slot == null || slot.item == null) return;

        inventory.RemoveItem(slot.item, slotIndex);
    }
}
