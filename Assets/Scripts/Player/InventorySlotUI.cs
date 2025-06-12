using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text quantityUI;
    [SerializeField] private Text itemNameUI;
    [SerializeField] private Text descriptionUI;

    public void InitializeSlot(Item item, int itemQuantity)
    {
        icon.sprite = item.itemIcon;
        itemNameUI.text = item.itemName;
        descriptionUI.text = item.itemDescription;
        quantityUI.text = itemQuantity.ToString();
    }

    public void UpdateSlot(int itemQuantity) 
    {
        quantityUI.text = itemQuantity.ToString();
    }
}
