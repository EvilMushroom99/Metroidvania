using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemDescription;
    [Range(1, 16)] public int stack;
    public Sprite itemIcon;

    public abstract void Use(GameObject user);
}
