using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    private CombatStats currentStats, baseStats;
    public Helmet helmet;
    public Armor armor;
    public Weapon weapon;
    public Accessory accessory1, accessory2;
    public GeneralActions generalActions;
    public List<Action> actions;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        baseStats = GetComponent<CombatStats>();
        currentStats = gameObject.AddComponent<CombatStats>();
        generalActions = new GeneralActions();
        actions = new List<Action>();

        SetCurrentStats();

        actions.AddRange(generalActions.actions);

        //set user of all actions
        foreach (Action action in actions)
        {
            action.user = this;
        }
    }

    public abstract void TakeTurn();

    private void TakeDamage(int damage)
    {
        currentStats.health -= damage;
        if (currentStats.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " has died.");
        Destroy(gameObject);
    }

    public void TakePhysicalHit(int damage)
    {
        damage -= currentStats.defense;
        if (damage < 0)
        {
            damage = 0;
        }
        TakeDamage(damage);
    }

    public void TakeMagicalHit(int damage)
    {
        damage -= currentStats.resistance;
        if (damage < 0)
        {
            damage = 0;
        }
        TakeDamage(damage);
    }

    public void Heal(int amount)
    {
        currentStats.health += amount;
    }

    public void TakeAction(Action action, Combatant target)
    {
        Debug.Log(name + " took action: " + action.actionName);
        action.TakeAction(target);
    }

    public void EquipHelmet(Helmet helmetItem)
    {
        if (helmet != null)
        {
            RemoveEquipmentStats(helmet);
        }
        helmet = helmetItem;
        AddEquipmentStats(helmet);
    }

    public void UnequipHelmet()
    {
        RemoveEquipmentStats(helmet);
        helmet = null;
    }

    public void EquipArmor(Armor armorItem)
    {
        if (armor != null)
        {
            RemoveEquipmentStats(armor);
        }
        armor = armorItem;
        AddEquipmentStats(armor);
    }

    public void UnequipArmor()
    {
        RemoveEquipmentStats(armor);
        armor = null;
    }

    public void EquipWeapon(Weapon weaponItem)
    {
        if (weapon != null)
        {
            RemoveEquipmentStats(weapon);
        }
        weapon = weaponItem;
        AddEquipmentStats(weapon);
    }

    public void UnequipWeapon()
    {
        RemoveEquipmentStats(weapon);
        weapon = null;
    }

    public void EquipAccessory1(Accessory accessoryItem)
    {
        if (accessory1 != null)
        {
            RemoveEquipmentStats(accessory1);
        }
        accessory1 = accessoryItem;
        AddEquipmentStats(accessory1);
    }

    public void UnequipAccessory1()
    {
        RemoveEquipmentStats(accessory1);
        accessory1 = null;
    }

    public void EquipAccessory2(Accessory accessoryItem)
    {
        if (accessory2 != null)
        {
            RemoveEquipmentStats(accessory2);
        }
        accessory2 = accessoryItem;
        AddEquipmentStats(accessory2);
    }

    public void UnequipAccessory2()
    {
        RemoveEquipmentStats(accessory2);
        accessory2 = null;
    }

    private void AddEquipmentStats(Equipment equipment)
    {
        currentStats.attackPower += equipment.attackPowerMod;
        currentStats.magicPower += equipment.magicPowerMod;
        currentStats.defense += equipment.defenseMod;
        currentStats.skill += equipment.skillMod;
        currentStats.speed += equipment.speedMod;
    }

    private void RemoveEquipmentStats(Equipment equipment)
    {
        currentStats.attackPower -= equipment.attackPowerMod;
        currentStats.magicPower -= equipment.magicPowerMod;
        currentStats.defense -= equipment.defenseMod;
        currentStats.skill -= equipment.skillMod;
        currentStats.speed -= equipment.speedMod;
    }

    public CombatStats GetStats()
    {
        return currentStats;
    }

    private void SetCurrentStats()
    {
        currentStats.health = baseStats.health;
        currentStats.attackPower = baseStats.attackPower;
        currentStats.magicPower = baseStats.magicPower;
        currentStats.defense = baseStats.defense;
        currentStats.skill = baseStats.skill;
        currentStats.speed = baseStats.speed;

        if (helmet != null)
        {
            AddEquipmentStats(helmet);
        }
        if (armor != null)
        {
            AddEquipmentStats(armor);
        }
        if (weapon != null)
        {
            AddEquipmentStats(weapon);
        }
        if (accessory1 != null)
        {
            AddEquipmentStats(accessory1);
        }
        if (accessory2 != null)
        {
            AddEquipmentStats(accessory2);
        }
    }
}
