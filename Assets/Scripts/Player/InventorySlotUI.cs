using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text quantityUI;
    [SerializeField] private Text itemNameUI;
    [SerializeField] private Text descriptionUI;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button useButton;

    private PlayerInventory inventory; 
    private Item item;

    public void InitializeSlot(Item itemReference, int itemQuantity, PlayerInventory playerInventory)
    {
        item = itemReference;
        icon.sprite = item.itemIcon;
        itemNameUI.text = item.itemName;
        descriptionUI.text = item.itemDescription;
        quantityUI.text = itemQuantity.ToString();
        inventory = playerInventory;
    }

    public void UpdateSlot(int itemQuantity) 
    {
        quantityUI.text = itemQuantity.ToString();
    }

    public void RemoveItem()
    {
        inventory.RemoveItem(item);
    }

    public void UseItem()
    {
        inventory.UseItem(item);
    }
}
