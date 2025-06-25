using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class InventoryPersistence : MonoBehaviour
{
    [SerializeField] private InventorySO inventory;

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        List<InventorySlot> slots = inventory.GetAllSlots();
        SlotDataList slotDataList = new();

        foreach (InventorySlot slot in slots) 
        {
            if (slot.item != null) 
            {
                SlotData slotData = new SlotData();
                slotData.slotIndex = slot.slotIndex;
                slotData.itemId = slot.item.id;
                slotData.itemQuantity = slot.quantity;
                slotDataList.slots.Add(slotData);
            }
        }

        string json = JsonUtility.ToJson(slotDataList, true);
        string fullpath = Path.Combine(Application.persistentDataPath, "inventory.json");
        File.WriteAllText(fullpath, json);
    }

    public void LoadData()
    {
        inventory.InitializeInventory();
        string fullpath = Path.Combine(Application.persistentDataPath, "inventory.json");
        if (File.Exists(fullpath))
        {
            string json = File.ReadAllText(fullpath);
            SlotDataList loadedList = JsonUtility.FromJson<SlotDataList>(json);
            inventory.LoadInventoryItems(loadedList.slots);
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}

[Serializable]
public class SlotData 
{
    public int slotIndex;
    public int itemId;
    public int itemQuantity;
}

[Serializable]
public class SlotDataList
{
    public List<SlotData> slots = new();
}


