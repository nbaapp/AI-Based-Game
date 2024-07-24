// RoomManager.cs
using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public RoomData[] rooms; // Array to hold rooms

    public Vector3 roomOrigin = Vector3.zero; // Origin where rooms will be instantiated

    private RoomData currentRoomData;
    private GameObject currentRoomInstance;

    //dictionary to store room state
    private Dictionary<Vector2Int, RoomState> roomStates = new Dictionary<Vector2Int, RoomState>();
    private void Start()
    {
        InitializeRoomStates();
    }

    private void InitializeRoomStates()
    {
        // Initialize room states
        foreach (var room in rooms)
        {
            room.roomState = new RoomState(room.coordinates, room);
            roomStates[room.coordinates] = room.roomState;
            room.roomState.roomData = room;
        }
    }

    public void LoadRoom(RoomData roomData)
    {
        // Unload the current room if there is one
        if (currentRoomInstance != null)
        {
            Destroy(currentRoomInstance);
        }

        // Instantiate the new room at the origin
        currentRoomInstance = Instantiate(roomData.roomPrefab, roomOrigin, Quaternion.identity);
        currentRoomData = roomData;
    }

    private void UpdateRoomState(RoomData roomData)
    {
        // Load the state data for the new room if needed
        RoomState state;
        if (roomStates.TryGetValue(roomData.coordinates, out state))
        {
            roomData.roomState = state;
        }
        else
        {
            roomData.roomState = new RoomState(roomData.coordinates, roomData);
            roomStates[roomData.coordinates] = roomData.roomState;
        }
    }


    public RoomData GetRandomRoom()
    {
        // Get a random room
        int randomIndex = Random.Range(0, rooms.Length);
        RoomData room = rooms[randomIndex];
        return room;
    }
}
