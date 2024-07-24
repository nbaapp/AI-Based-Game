using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerCombatant : Combatant
{
    public UnityEvent PlayerTurnCompleted;
    private CombatUI combatUI;

    public void Awake()
    {
        if (PlayerTurnCompleted == null) PlayerTurnCompleted = new UnityEvent();

        combatUI = FindObjectOfType<CombatUI>();

    }

    public override void TakeTurn()
    {
        Debug.Log(name + " (Player) is taking their turn.");
        // Display the UI for the player's turn
        combatUI.ShowPlayerUI();
        // Wait for player input and then call FinishTurn

    }

    public void Act()
    {
        FinishTurn(); // Placeholder for player input
    }

    public void FinishTurn()
    {
        // Hide the UI for the player's turn
        combatUI.HidePlayerUI();
        // Notify that the player has completed their turn
        if (PlayerTurnCompleted != null)
        {
            PlayerTurnCompleted.Invoke();
        }
    }
}
