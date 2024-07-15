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
        Action action = Actions[Random.Range(0, Actions.Count)];
        // Select a target
        Combatant target = Enemies[Random.Range(0, Enemies.Count)];
        // Take the action
        TakeAction(action, target);
    }
}
