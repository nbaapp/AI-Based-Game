// PlayerSpawnManager.cs
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 defaultSpawnPosition; // Default position to place the player

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = defaultSpawnPosition;
        }
    }
}
