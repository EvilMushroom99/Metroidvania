using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private GameObject slotPrefab;

    public void AddItem(Item item)
    {
        GameObject newSlot = Instantiate(slotPrefab, inventoryParent);
        newSlot.GetComponent<InventorySlotUI>().InitializeSlot(item, 1);

    }

    public void ChangeSlotQuantity(int quantity, int index)
    {
        inventoryParent.GetChild(index).GetComponent<InventorySlotUI>().UpdateSlot(quantity);
    }

    public void DeleteSlot(int index)
    {
        Destroy(inventoryParent.GetChild(index).gameObject);
    }

    public void ChangeSlotPosition()
    {

    }

    public void SwapSlots()
    {

    }
}
