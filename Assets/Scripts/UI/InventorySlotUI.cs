using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventorySO inventory;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemUI newitemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            inventory.SwapOrStackItem(newitemUI.item, newitemUI.quantity, transform.GetSiblingIndex(), newitemUI.slotIndex);
        }
    }

    //OLD
    /*
    public void InitializeSlot(Item itemReference, int itemQuantity)
    {
        itemUI.InitializeItemUI(itemReference, itemQuantity);
        itemUI.slotIndex = index;
    }

    public void UpdateSlot(int itemQuantity) 
    {
        itemUI.UpdateQuantity(itemQuantity);
    }

    public void ClearSlot()
    {
        itemUI.ClearItemUI();
    }

    public void EndDrag()
    {
        itemUI.ForceEndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            ItemUI newitemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            PlayerInventory.Instance.SwapOrStackItem(newitemUI.item, newitemUI.quantity, index, newitemUI.slotIndex);
        }
    }
    */
}
