using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private GameObject slotPrefab;

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

    public void AddItem(Item item, int index, int quantity)
    {
        Debug.Log("adding Item to: " + index + " el item: " + item + " y la cantidad: " + quantity);
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
}
