using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomState
{
    public List<Character> characters; // Characters in the room
    public List<Item> items; // Items in the room
    public Vector2Int coordinates; // The coordinates of the room in the grid
    public RoomData roomData; // The data for this room

    public RoomState(Vector2Int coordinates, RoomData roomData)
    {
        this.coordinates = coordinates;
        this.roomData = roomData;
        characters = new List<Character>();
        items = new List<Item>();
    }

    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }

    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }
}