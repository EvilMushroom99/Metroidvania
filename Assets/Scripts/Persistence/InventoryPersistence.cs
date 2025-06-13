using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class InventoryPersistence : MonoBehaviour
{
    private string fullpath;

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        List<InventorySlot> slots = PlayerInventory.Instance.GetInventory();
        SlotDataList slotDataList = new SlotDataList();

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
        File.WriteAllText(fullpath, json);
    }

    public void LoadData()
    {
        PlayerInventory.Instance.InitializeInventory();
        fullpath = Path.Combine(Application.persistentDataPath, "inventory.json");
        string json = File.ReadAllText(fullpath);
        SlotDataList loadedList = JsonUtility.FromJson<SlotDataList>(json);
        PlayerInventory.Instance.LoadInventoryItems(loadedList.slots);
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
    public List<SlotData> slots = new List<SlotData>();
}


