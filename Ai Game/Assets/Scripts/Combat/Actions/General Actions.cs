using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralActions
{
    public List<Action> actions;

    //constructor to add all general actions to general actions
    public GeneralActions()
    {
        actions = new List<Action>
        {
            new Attack(),
            //new Defend()
        };
    }
}

public class Attack : Action
{
    public Attack()
    {
        actionName = "Attack";
        actionDescription = "Basic attack against a single target";
    }
    public override void TakeAction(Combatant target)
    {
        target.TakePhysicalHit(user.GetStats().attackPower);
    }
}

public class Defend : Action
{
    public override void TakeAction(Combatant target)
    {
        Debug.Log("Defended");
    }
}
