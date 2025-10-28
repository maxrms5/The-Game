using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    public Inventory inventory;
    public static int manaPotions;

    void Awake()
    {
        inventory.LoadFromJson();
    }
    private void OnDestroy()
    {
        inventory.SaveToJson();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //Saves game data
        {
            inventory.SaveToJson();
        }

        if (Input.GetKeyDown(KeyCode.L)) //Loads game data
        {
            inventory.LoadFromJson();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            inventory.ResetSaveJson();
        }
    }
    public void PickupItem(Item.Type type)
    {
        Debug.Log("Searching for item");
        Item item = inventory.items.Find(q => q.type == type); //Looks for existing item

        if (item == null) //makes item if item does not already exist
        {
            Debug.Log("Item Does not exist");
            Item newItem = new Item()
            {
                id = System.Guid.NewGuid(),
                type = type,
                count = 0,
            };
            inventory.items.Add(newItem); 
            newItem.count++;
            Debug.Log("Item instantiated");
        }
        else //if item exists, increases the ammount of items held by 1
        {
            Debug.Log("Item Exists");
            //Debug.Log(item.type + " Current Ammount: " + item.count);
            item.count++;
            Debug.Log("Item count increased");
            //Debug.Log(item.type + " New Ammount: " + item.count);
        }
        Debug.Log("Item Added");
    }
    public int ItemCount(Item.Type type) //returns count of an item for UI purposes
    {
        Item item = inventory.items.Find(q => q.type == type);
        //Debug.Log(item.type + " Count is " + item.count);
        return item.count;
    }
    public void UseItem(Item.Type type)
    {
        // find the item of that type in the inventory
        Item item = inventory.items.Find(q => q.type == type);
        if (item == null)
        {
            Debug.LogFormat("No item in inventory", type);
            return;
        }
        // decrease count
        item.count--;
        if (item.count <= 0)
        {
            inventory.items.Remove(item);
            Debug.LogFormat("Removed from inventory", type);
        }
        else
        {
            Debug.LogFormat("-1 Item");
        }
    }
}

[System.Serializable]
public class Inventory
{
    public List<Item> items = new List<Item> ();
    public void SaveToJson() //Saves data to a local Json file
    {
        string inventoryData = JsonUtility.ToJson(this);
        string filePath = Application.persistentDataPath + "/InventoryData.Json";
        Debug.Log(filePath);
        File.WriteAllText(filePath, inventoryData);
        Debug.Log("Save Effective");
    }
    public void LoadFromJson() //Loads game data from a local Json file
    {
        string filePath = Application.persistentDataPath + "/InventoryData.Json";
        JsonUtility.FromJsonOverwrite(File.ReadAllText(filePath), this);
        Debug.Log("Load Effective");
    }

    public void ResetSaveJson() //Sets all variable values in local Json file to 0
    {
        string filePath = Application.persistentDataPath + "/InventoryData.Json";
        File.Delete(filePath);
    }
}

[System.Serializable]
public class Item
{
    public enum Type { HealthPotion, ManaPotion, GoldCoin } //List of possible items
    public System.Guid id; //id for each item
    public Type type; //name of item 
    public int count; //number of items in inventory
}