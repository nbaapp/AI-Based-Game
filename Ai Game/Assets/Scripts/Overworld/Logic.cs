using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Logic : MonoBehaviour
{
    Map map;
    PlayerCharacter player;

    [SerializeField] List<NPC> npcs;

    private AIManager ai;
    public Scenario startScenario;

    //full of scenarios with name structure: name [x,y]
    public List<Scenario> scenarios;
    private UIManager uiManager;
    private AIManager aiManager;
    public int numberOfNPCs; 
    
    private void Start()
    {
        map = FindObjectOfType<Map>();
        player = FindObjectOfType<PlayerCharacter>();
        ai = FindObjectOfType<AIManager>();
        uiManager = FindObjectOfType<UIManager>();
        aiManager = FindObjectOfType<AIManager>();

        aiManager.DoneWithSetup.AddListener(SetupGame);
        aiManager.ResponseRecieved.AddListener(UodateUI);
    }

    private void SetupGame()
    {
        npcs = aiManager.npcs;
        //move each npc to a random scenario
        foreach (NPC npc in npcs)
        {
            MoveToScenario(npc, scenarios[UnityEngine.Random.Range(0, scenarios.Count)]);
        }
        MoveToScenario(player, startScenario);
        BeginScenario();
    }

    public bool IsValidDirection(Direction direction)
    {
        //check if scenario has valid exit in direction
        if (player.currentScenario.GetExit(direction) != null)
        {
            if (player.currentScenario.GetExit(direction).locked)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public Scenario GetScenarioFromCoordinates(int x, int y)
    {
        //find scenario from the list
        foreach (Scenario scenario in scenarios)
        {
            if (scenario.coordx == x && scenario.coordy == y)
            {
                Debug.Log("Found Scenario: " + scenario.scenarioName);
                return scenario;
            }
        }
        return null;
    }

    public void MoveToScenario(Character character, Scenario scenario)
    {
        //leave current scenario
        if (character.currentScenario != null) {
            character.currentScenario.RemoveCharacter(character);
        }

        //enter new scenario    
        character.currentScenario = scenario;
        scenario.AddCharacter(character);

        Debug.Log(character.characterName + " moved to " + scenario.scenarioName);
    }

    public void MoveToScenario(PlayerCharacter character, Scenario scenario)
    {
        //update all characters in scenario to have met player
        if (character.currentScenario != null) {
            foreach (NPC npc in npcs) {
                if (npc.currentScenario != null) {
                    if (npc.currentScenario == character.currentScenario)
                    {
                        npc.SetMetPlayer(true);
                    }
                }
            }
            //leave current scenario
            character.currentScenario.RemoveCharacter(character);
        }
        
        //enter new scenario
        character.currentScenario = scenario;
        scenario.AddCharacter(character);

        Debug.Log(character.characterName + " moved to " + scenario.scenarioName);
    }
    
    public void MoveToScenario(Character character, Direction direction)
    {
        //leave current scenario
        character.currentScenario.RemoveCharacter(character);

        Scenario scenario = character.currentScenario.GetExit(direction).destination;
        character.currentScenario = scenario;
        scenario.AddCharacter(character);

        Debug.Log(character.characterName + " moved " + direction + " to " + scenario.scenarioName);
    }

    public void MoveToScenario(PlayerCharacter character, Direction direction)
    {
        //update all characters in scenario to have met player
        if (character.currentScenario != null) {
            foreach (NPC npc in npcs) {
                if (npc.currentScenario != null) {
                    if (npc.currentScenario == character.currentScenario)
                    {
                        npc.SetMetPlayer(true);
                    }
                }
            }
            //leave current scenario
            character.currentScenario.RemoveCharacter(character);
        }

        Scenario scenario = character.currentScenario.GetExit(direction).destination;
        character.currentScenario = scenario;

        scenario.AddCharacter(character);
        Debug.Log(character.characterName + " moved " + direction + " to " + scenario.scenarioName);
    }

    

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

    public void UodateUI(string message)
    {
        uiManager.SetAIText(message);
    }

    public void MoveAllNPCs()
    {
        foreach (NPC npc in npcs)
        {
            //get valid exit directions from current scenario
            List<Direction> validDirections = new List<Direction>();
            foreach (Exit exit in npc.currentScenario.exits)
            {
                if (!exit.locked)
                {
                    validDirections.Add(exit.direction);
                }
            }

            //randomly choose a direction, or choose to not move.
            int randomIndex = UnityEngine.Random.Range(0, validDirections.Count + 1);
            if (randomIndex < validDirections.Count)
            {
                MoveToScenario(npc, validDirections[randomIndex]);
            }
            else
            {
                Debug.Log(npc.characterName + " did not move.");
            }
        }
    }

    private void CheckAllDirections()
    {
        if (IsValidDirection(Direction.North))
        {
            uiManager.EnableDirection(Direction.North);
        }
        else
        {
            uiManager.DisableDirection(Direction.North);
        }
        if (IsValidDirection(Direction.South))
        {
            uiManager.EnableDirection(Direction.South);
        }
        else
        {
            uiManager.DisableDirection(Direction.South);
        }
        if (IsValidDirection(Direction.East))
        {
            uiManager.EnableDirection(Direction.East);
        }
        else
        {
            uiManager.DisableDirection(Direction.East);
        }
        if (IsValidDirection(Direction.West))
        {
            uiManager.EnableDirection(Direction.West);
        }
        else
        {
            uiManager.DisableDirection(Direction.West);
        }
    }
}
