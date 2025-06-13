using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform inventoryParent;

    public void InitializeSlots()
    {
        for (int i = 0; i < inventoryParent.childCount; i++)
        {
            inventoryParent.GetChild(i).GetComponent<InventorySlotUI>().SetComponents();
        }
    }

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
}
