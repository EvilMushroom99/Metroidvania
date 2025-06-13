using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    private ItemUI itemUI;
    private int index;

    public void SetComponents()
    {
        index = transform.GetSiblingIndex();
        itemUI = GetComponentInChildren<ItemUI>();
        itemUI.SetComponents();
    }

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

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            ItemUI newitemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            PlayerInventory.Instance.InsertItem(newitemUI.item, newitemUI.quantity, index, newitemUI.slotIndex);
        }
    }
}
