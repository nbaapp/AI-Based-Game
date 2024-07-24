using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCombatant : Combatant
{
    public List<Combatant> Allies;
    public List<Combatant> Enemies;

    public void Act()
    {
        // Select an action to take
        Action action = null;
        Combatant target = null;

        if (actions.Count > 0) {
            action = actions[Random.Range(0, actions.Count)];
        }
        
        if (Enemies.Count > 0) {
            target = Enemies[Random.Range(0, Enemies.Count)];
        }

        if (action != null && target != null) {
            TakeAction(action, target);
        }
        else {
            Debug.Log(name + " failed to act");
        }
    }

    public override void TakeTurn()
    {
        Debug.Log(name + " (Agent) is taking their turn.");
        Act();
    }
}
