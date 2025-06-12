using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int stack;
    public Sprite itemIcon;

    public virtual void Use()
    {
        Debug.Log("Usando Item: " + itemName);
    }
}
