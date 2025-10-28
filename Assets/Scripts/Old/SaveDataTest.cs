using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*public class SaveDataTest : MonoBehaviour
{
    public Inventory inventory;
    public static int manaPotions;
    //public int itemCount;
    ItemCollector collector;

    void Start()            
    {
        inventory.LoadFromJson();
        inventory.items.Clear();
        inventory.items.Add("HealthPotions", 0);
        inventory.items.Add("ManaPotions", 0);
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
            PickupItem(Item.Type.HealthPotion);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem(Item.Type.HealthPotion);
        }
    }
    public void PickupItem(string item)
    {
        int itemCount;
        Debug.Log("Searching for item");

        if (inventory.items.TryGetValue(item, out itemCount)) //makes item if item does not already exist
        {
            Debug.Log("Item Does not exist");
            itemCount++;
            inventory.items.Add(item, itemCount); 
            Debug.Log("Item instantiated");
        }
        else //if item exists, increases the ammount of items held by 1
        {
            Debug.Log("Item Exists");
            Debug.Log("Current Ammount: " );
           
            Debug.Log("Item count increased");
            Debug.Log("New Ammount: " );
        }
        Debug.Log("Item Added");
        //Debug.Log(ItemCount());
    }
    public IEnumerator ItemCount()
    {
        Debug.Log("Item Count is " );
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
    public Dictionary<string, int> items = new Dictionary<string, int>();
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

    public void DeleteSaveJson() //Deletes local Json file
    {
        string filePath = Application.persistentDataPath + "/InventoryData.Json";
        File.Delete(filePath);
    }
}

[System.Serializable]
public class Item
{
    public enum Type { HealthPotion, ManaPotion } //List of possible items
    public System.Guid id; //id for each item
    public Type type; //name of item 
    public int count; //number of items in inventory
}*/