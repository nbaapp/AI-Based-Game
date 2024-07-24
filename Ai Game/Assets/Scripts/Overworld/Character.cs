using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public List<Item> inventory;
    private RoomData currentRoom;

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

    public void SetRoom(RoomState room)
    {
        room.AddCharacter(this);
    }

    public RoomData GetRoom()
    {
        return currentRoom;
    }
}
