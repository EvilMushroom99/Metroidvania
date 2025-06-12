using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private Text quantityUI;
    [SerializeField] private Text itemNameUI;
    [SerializeField] private Text descriptionUI;
    [SerializeField] private GameObject descriptionParentUI;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button useButton;

    [SerializeField] private PlayerInventory inventory; 
    private Item item;

    public void InitializeSlot(Item itemReference, int itemQuantity)
    {
        GetComponent<Image>().raycastTarget = true;
        item = itemReference;
        icon.sprite = item.itemIcon; 
        icon.enabled = true;
        itemNameUI.text = item.itemName;
        descriptionUI.text = item.itemDescription;
        quantityUI.text = itemQuantity.ToString();
    }

    public void UpdateSlot(int itemQuantity) 
    {
        quantityUI.text = itemQuantity.ToString();
    }

    public void RemoveItem()
    {
        inventory.RemoveItem(item, transform.GetSiblingIndex());
    }

    public void UseItem()
    {
        inventory.UseItem(item, transform.GetSiblingIndex());
    }

    public void ClearSlot()
    {
        GetComponent<Image>().raycastTarget = false;
        icon.enabled = false;
        icon.sprite = null;
        descriptionUI.text = null;
        quantityUI.text = null;
        itemNameUI.text = null;
        item = null;
        removeButton.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        removeButton.gameObject.SetActive(true);
        descriptionParentUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        removeButton.gameObject.SetActive(false);
        descriptionParentUI.SetActive(false);
    }
}
