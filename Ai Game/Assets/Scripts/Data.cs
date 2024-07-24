using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    North,
    South,
    East,
    West
}

public abstract class Item : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
}

public abstract class Equipment : Item
{
    public int healthMod;
    public int attackPowerMod;
    public int magicPowerMod;
    public int defenseMod;
    public int skillMod;
    public int speedMod;
}

public class Helmet : Equipment
{
}

public class Armor : Equipment
{
}

public class Weapon : Equipment
{
}

public class Accessory : Equipment
{
}

public abstract class Action
{
    public string actionName;
    public string actionDescription;
    public Combatant user;
    abstract public void TakeAction(Combatant target);

    public Action()
    {
        actionName = "UNDEFINED";
        actionDescription = "UNDEFINED";
        user = null;
    }
}