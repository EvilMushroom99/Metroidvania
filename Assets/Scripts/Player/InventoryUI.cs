using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private PlayerInventory inventory;

    private void Awake()
    {
        ClearSlots();
    }

    public void ClearSlots()
    {
        for (int i = 0; i < inventoryParent.childCount; i++)
        {
            inventoryParent.GetChild(i).GetComponent<InventorySlotUI>().ClearSlot();
        }
    }

    public void AddItem(Item item, int index)
    {
        InventorySlotUI slot = inventoryParent.GetChild(index).GetComponent<InventorySlotUI>();
        slot.InitializeSlot(item, 1);
    }

    public void ChangeSlotQuantity(int quantity, int index)
    {
        inventoryParent.GetChild(index).GetComponent<InventorySlotUI>().UpdateSlot(quantity);
    }

    public void DeleteSlot(int index)
    {
        inventoryParent.GetChild(index).GetComponent<InventorySlotUI>().ClearSlot();
    }

    public void ChangeSlotPosition()
    {

    }

    public void SwapSlots()
    {

    }
}
