using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public string scenarioName;
    [TextArea(15,20)] public string scenarioDescription;
    public string scenarioMessages = "";
    public int coordx;
    public int coordy;
    public List<Exit> exits;
    public List<Item> items;
    public List<Character> characters;
    private OpenAIController openAI;

    private void Start()
    {
        openAI = GameObject.Find("OpenAI").GetComponent<OpenAIController>();
    }


    public Scenario(string name, string description, int coordx, int coordy)
    {
        scenarioName = name;
        scenarioDescription = description;
        this.coordx = coordx;
        this.coordy = coordy;
        exits = new List<Exit>();
        items = new List<Item>();
        characters = new List<Character>();
    }

    public void AddExit(Exit exit)
    {
        exits.Add(exit);
    }

    public void RemoveExit(Exit exit)
    {
        exits.Remove(exit);
    }

    public Exit GetExit(Direction direction)
    {
        foreach (Exit exit in exits)
        {
            if (exit.direction == direction)
            {
                return exit;
            }
        }
        return null;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public Item GetItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }

    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        characters.Remove(character);
    }

    public Character GetCharacter(string characterName)
    {
        foreach (Character character in characters)
        {
            if (character.characterName == characterName)
            {
                return character;
            }
        }
        return null;
    }

    public string GetDescription()
    {
        return scenarioDescription;
    }

    public Tuple<int, int> GetCoordinates()
    {
        return new Tuple<int, int>(coordx, coordy);
    }

    public void SetCoordinates(int x, int y)
    {
        coordx = x;
        coordy = y;
    }

    public void SetDescription(string description)
    {
        scenarioDescription = description;
    }

    public void SetName(string name)
    {
        scenarioName = name;
    }

    public string GetName()
    {
        return scenarioName;
    }
}
