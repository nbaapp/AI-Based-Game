using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    private CombatStats stats;
    public Helmet helmet;
    public Armor armor;
    public Weapon weapon;
    public Accessory accessory1, accessory2;
    public List<Action> Actions;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CombatStats>();
    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
    }

    public void Heal(int amount)
    {
        stats.health += amount;
    }

    public void TakeAction(Action action, Combatant target)
    {
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
        stats.attackPower += equipment.attackPowerMod;
        stats.magicPower += equipment.magicPowerMod;
        stats.defense += equipment.defenseMod;
        stats.skill += equipment.skillMod;
        stats.speed += equipment.speedMod;
    }

    private void RemoveEquipmentStats(Equipment equipment)
    {
        stats.attackPower -= equipment.attackPowerMod;
        stats.magicPower -= equipment.magicPowerMod;
        stats.defense -= equipment.defenseMod;
        stats.skill -= equipment.skillMod;
        stats.speed -= equipment.speedMod;
    }

    public CombatStats GetStats()
    {
        return stats;
    }
}
