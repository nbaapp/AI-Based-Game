using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    private List<NPC> npcs;
    private RoomManager roomManager;
    private PlayerCharacter player;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        player = FindObjectOfType<PlayerCharacter>();
    }

    public void SetNPCs(List<NPC> list)
    {
        npcs = list;
    }

    public void MoveToRoom(Character character, RoomData room)
    {
        //leave current room
        // check if character is in a valid room
        RoomData currentRoom = character.GetRoom();
        if (currentRoom != null)
        {
            currentRoom.RemoveCharacter(character);
        }

        //enter new scenario
        character.SetRoom(room.roomState);
        room.AddCharacter(character);

        Debug.Log(character.characterName + " moved to room " + room.coordinates);
    }

    public void MovePlayerToRoom(PlayerCharacter character, RoomState room)
    {
        //update all characters in scenario to have met player

        RoomData currentRoom = character.GetRoom();

        if (currentRoom != null) {
            foreach (NPC npc in npcs) {
                RoomData npcRoom = npc.GetRoom();
                if (npcRoom != null) {
                    if (npcRoom == currentRoom)
                    {
                        npc.SetMetPlayer(true);
                    }
                }
            }
            //leave current scenario
            character.GetRoom().RemoveCharacter(character);
        }
        
        //enter new scenario
        character.SetRoom(room);
        room.AddCharacter(character);
        roomManager.LoadRoom(room.roomData);
        player.SetPlayerPosition(room.roomData.playerSpawnPoint);
        

        Debug.Log(character.characterName + " moved to " + room.coordinates);
    }
}
