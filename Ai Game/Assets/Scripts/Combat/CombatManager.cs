using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<Combatant> combatants;
    public CombatUI combatUI;
    public float turnDelay = 1f;
    private int currentTurnIndex = 0;
    private bool playerTurnCompleted = false;

    void Start()
    {
        combatUI.HidePlayerUI();

        // Initialize the combatants
        combatants = new List<Combatant>(FindObjectsOfType<Combatant>());
        foreach (Combatant combatant in combatants)
        {
            combatant.Initialize();
        }

        // Sort the combatants by speed in descending order
        combatants.Sort((a, b) => b.GetStats().speed.CompareTo(a.GetStats().speed));


        // Start the turn system
        StartCoroutine(TakeTurns());
    }

    private IEnumerator TakeTurns()
    {
        while (combatants.Count > 0)
        {
            Combatant currentCombatant = combatants[currentTurnIndex];

            if (currentCombatant != null)
            {
                if (currentCombatant is PlayerCombatant playerCombatant)
                {
                    // Subscribe to the player's turn completion event
                    playerCombatant.PlayerTurnCompleted.AddListener(OnPlayerTurnCompleted);
                    
                    // Player takes their turn
                    playerCombatant.TakeTurn();

                    // Wait until the player completes their turn
                    yield return new WaitUntil(() => playerTurnCompleted);
                    
                    // Unsubscribe to avoid memory leaks
                    playerCombatant.PlayerTurnCompleted.RemoveListener(OnPlayerTurnCompleted);

                    // Reset the playerTurnCompleted flag
                    playerTurnCompleted = false;
                }
                else
                {
                    // NPC takes their turn
                    currentCombatant.TakeTurn();

                    // Wait until the combatant's action is completed
                    // This can be implemented with a coroutine, events, or other async methods
                    yield return new WaitForSeconds(turnDelay); // Simulate waiting time for the combatant's action
                }
            }

            // Move to the next combatant's turn
            currentTurnIndex = (currentTurnIndex + 1) % combatants.Count;
        }
    }


    private void OnPlayerTurnCompleted()
    {
        playerTurnCompleted = true;
    }
}
