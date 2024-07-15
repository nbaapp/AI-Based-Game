using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.VersionControl;
using UnityEngine;

public class NPC : Character
{
    public string goals;
    public string personality;
    public string physicalDescription;
    private bool hasMetPlayer = false;

    public string PrintData()
    {
        string data = "Name: " + characterName + "\n" +
                      "\t" + "PhysicalDescription: " + physicalDescription + "\n" +
                      "\t" + "Personality: " + personality + "\n" +
                      "\t" + "Personal Goals: " + goals + "\n" +
                      "\t" + "Encountered Player Before: ";

        if (hasMetPlayer) {
            data += "Yes";
        } else {
            data += "No";
        }
        
        return data;
    }

    public void SetMetPlayer(bool metPlayer)
    {
        hasMetPlayer = metPlayer;
    }

    public bool HasMetPlayer()
    {
        return hasMetPlayer;
    }
}
