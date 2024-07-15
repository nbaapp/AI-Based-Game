using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private Logic logic;
    private UIManager uiManager;

    void Start()
    {
        logic = FindObjectOfType<Logic>();
    }

    public void GoNorth()
    {
        logic.MoveToScenario(this, Direction.North);
        logic.MoveAllNPCs();
        uiManager.DisableDirections();
        logic.BeginScenario();
    }

    public void GoSouth()
    {
        logic.MoveToScenario(this, Direction.South);
        logic.MoveAllNPCs();
        uiManager.DisableDirections();
        logic.BeginScenario();
    }

    public void GoEast()
    {
        logic.MoveToScenario(this, Direction.East);
        logic.MoveAllNPCs();
        uiManager.DisableDirections();
        logic.BeginScenario();
    }

    public void GoWest()
    {
        logic.MoveToScenario(this, Direction.West);
        logic.MoveAllNPCs();
        uiManager.DisableDirections();
        logic.BeginScenario();
    }
}
