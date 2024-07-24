using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Logic : MonoBehaviour
{
    PlayerCharacter player;

    [SerializeField] List<NPC> npcs;

    private AIManager ai;
    public GameObject startRoomPrefab;

    public RoomManager roomManager;
    private UIManager uiManager;
    private AIManager aiManager;
    private CharacterTracker characterTracker;
    public int numberOfNPCs; 
    public Vector3 defaultSpawnPosition;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
        ai = FindObjectOfType<AIManager>();
        uiManager = FindObjectOfType<UIManager>();
        aiManager = FindObjectOfType<AIManager>();
        characterTracker = FindObjectOfType<CharacterTracker>();
        roomManager = FindObjectOfType<RoomManager>();

        aiManager.DoneWithSetup.AddListener(SetupGame);
        aiManager.ResponseRecieved.AddListener(UodateUI);
    }

    private void SetupGame()
    {
        npcs = aiManager.npcs;
        //move each npc to a random scenario
        foreach (NPC npc in npcs)
        {
            characterTracker.MoveToRoom(npc, roomManager.GetRandomRoom());
        }
        characterTracker.MovePlayerToRoom(player, startRoomPrefab.GetComponent<RoomData>().roomState);
        player.EnableControls();

        //BeginScenario();
    }
    
/*
    public async void BeginScenario()
    {
        Scenario scenario = player.currentScenario;
        int count = 0;

        string message = "Entered New Scenario: " + scenario.scenarioName + ": " + scenario.scenarioDescription + "\n";
        message += "NPCs:" + "\n";
        foreach (NPC npc in npcs)
        {
            if (npc.currentScenario == scenario)
            {
                count++;
                message += npc.PrintData() + "\n";
            }
        }
        if (count == 0)
        {
            message += "None" + "\n";
        }
        count = 0;
        message += "Exits:" + "\n";
        foreach (Exit exit in scenario.exits)
        {
            count++;
            message += exit.direction + ": " + exit.destination.scenarioName + "\n";
        }
        if (count == 0)
        {
            message += "None" + "\n";
        }

        Debug.Log(message);
        string aiResponse = await ai.SendMessageToThread(message, ai.storyThread);
        UodateUI(aiResponse);
        CheckAllDirections();
    }
    */

    public void UodateUI(string message)
    {
        uiManager.SetAIText(message);
    }

    public void DisablePlayerControls()
    {
        player.DisableControls();
    }

    public void EnablePlayerControls()
    {
        player.EnableControls();
    }

}
