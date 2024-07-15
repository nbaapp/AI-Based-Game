using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<Combatant> combatants;
    public List<AgentCombatant> agents;
    public PlayerCombatant player;
    public float turnDelay = 1f;

    //determine turn order based on speed
    public void DetermineTurnOrder()
    {
        //sort combatants by speed
        combatants.Sort((a, b) => b.GetStats().speed.CompareTo(a.GetStats().speed));
    }

    public void StartCombat()
    {
        //determine turn order
        DetermineTurnOrder();
        
    }

    //run through combatants and have them take their turns, always making sure the next combatant is at the front of the list.
    public IEnumerator RunCombat()
    {
        while (combatants.Count > 1)
        {
            //get the next combatant
            Combatant currentCombatant = combatants[0];
            //find combatant in lists
            if (agents.Contains(currentCombatant as AgentCombatant))
            {
                AgentCombatant agent = currentCombatant as AgentCombatant;
                agent.Act();
            }
            else if (currentCombatant == player)
            {
                combatants.RemoveAt(0);
                combatants.Add(currentCombatant);
                StartPlayerTurn();
                yield break;
            }
            else
            {
                Debug.LogError("Combatant not found in agents or player");
            }

            //move the combatant to the back of the list
            combatants.RemoveAt(0);
            combatants.Add(currentCombatant);
        }
    }

    private void StartPlayerTurn()
    {
        //enable player controls
    }
    
}
