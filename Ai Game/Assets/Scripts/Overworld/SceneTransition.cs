// SceneTransition.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public GameObject targetRoomPrefab; // The target room
    private RoomManager roomManager;
    public Vector3 playerSpawnPoint; // The spawn point for the player

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            roomManager.LoadRoom(targetRoomPrefab.GetComponent<RoomData>());
            player.SetPlayerPosition(playerSpawnPoint);
        }
    }
}
