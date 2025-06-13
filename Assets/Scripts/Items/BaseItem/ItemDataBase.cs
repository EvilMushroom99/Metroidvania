using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/ItemDatabase")]
public class ItemDataBase : ScriptableObject
{
    public Item[] items;

    public Item GetItemById(int id)
    {
        if (id >= 0 && id < items.Length)
            return items[id];

        Debug.LogWarning($"ID {id} out of range");
        return null;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].id != i)
            {
                Debug.LogWarning($"The Item at position {i} has id = {items[i].id}, it should be {i}.", items[i]);
            }
        }
    }
#endif
}
