using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public List<Item> inventory;
    public Scenario currentScenario;

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }

    public Item GetItem(string itemName)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }
}
