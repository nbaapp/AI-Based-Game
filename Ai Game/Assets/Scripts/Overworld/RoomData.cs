// RoomData.cs
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class RoomData : MonoBehaviour
{
    public Vector2Int coordinates; // The coordinates of the room in the grid
    public GameObject roomPrefab; // The prefab to instantiate for this room
    public RoomState roomState; // state of the room
    public Vector3 playerSpawnPoint; // The spawn point for the player

    public void AddCharacter(Character character)
    {
        roomState.AddCharacter(character);
    }

    public void RemoveCharacter(Character character)
    {
        roomState.RemoveCharacter(character);
    }
}