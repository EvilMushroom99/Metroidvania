using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private InventorySO inventory;

    [Header("ItemUI Components")]
    [SerializeField] private RectTransform itemDragHandler;
    [SerializeField] private Image icon;
    [SerializeField] private Image pickArea;
    [SerializeField] private Text quantityUI;
    [SerializeField] private Text itemNameUI;
    [SerializeField] private Text descriptionUI;
    [SerializeField] private GameObject descriptionParentUI;
    [SerializeField] private Button removeButton;

    private InventoryManager manager;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalSlot;

    public Item item;
    public int quantity;
    public int slotIndex;

    public void InitializeItemUI(InventoryManager inventoryManager, int index)
    {
        manager = inventoryManager;
        slotIndex = index;
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalSlot = transform.parent;
    }

    public void Refresh()
    {
        InventorySlot slot = inventory.GetSlot(slotIndex);

        if (!slot.item)
        {
            ClearItemUI();
            return;
        }

        if (slot.item != item)
        {
            SetData(slot.item, slot.quantity);
        }
        else if (slot.quantity != quantity)
        {
            UpdateQuantity(slot.quantity);
        }
    }
    
    private void SetData(Item itemReference, int itemQuantity)
    {
        pickArea.enabled = true;
        item = itemReference;
        quantity = itemQuantity;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        itemNameUI.text = item.itemName;
        descriptionUI.text = item.itemDescription;
        quantityUI.text = itemQuantity.ToString();
        rectTransform.anchoredPosition = Vector2.zero;
    }

    private void UpdateQuantity(int itemQuantity)
    {
        quantity = itemQuantity;
        quantityUI.text = itemQuantity.ToString();
    }

    private void ClearItemUI()
    {
        pickArea.enabled = false;
        icon.enabled = false;
        icon.sprite = null;
        descriptionUI.text = null;
        quantityUI.text = null;
        itemNameUI.text = null;
        item = null;
        quantity = 0;
        DisableSlotElements();
        ResetPosition();
    }

    private void EnableSlotElements()
    {
        removeButton.gameObject.SetActive(true);
        descriptionParentUI.SetActive(true);
    }

    private void DisableSlotElements()
    {
        removeButton.gameObject.SetActive(false);
        descriptionParentUI.SetActive(false);
    }

    private void ResetPosition()
    {
        transform.SetParent(originalSlot);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void RemoveItem()
    {
        manager.RemoveItem(slotIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!eventData.pointerDrag)
        {
            EnableSlotElements();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisableSlotElements();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        rectTransform.SetParent(itemDragHandler);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        ResetPosition();
        DisableSlotElements();
    }

    public void ForceEndDrag()
    {
        canvasGroup.blocksRaycasts = true;
        ResetPosition();
        DisableSlotElements();
    }
    
}
