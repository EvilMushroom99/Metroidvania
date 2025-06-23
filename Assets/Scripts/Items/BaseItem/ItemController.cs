using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;
    [SerializeField] private Item item;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemIcon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.AddItem(item);
            AudioManager.Instance.PlayPick();
            Destroy(gameObject);
        }
    }
}
