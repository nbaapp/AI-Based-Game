using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WorldInfo
{
    public string worldName;
    public string worldState;
    public string startLocation;
    public string description;

    public string PrintData()
    {
        string data = "Name: " + worldName + "\n" +
                      "\t" + "State: " + worldState + "\n" +
                      "\t" + "Starting Location: " + startLocation + "\n" +
                      "\t" + "Description: " + description;
        return data;
    }
}
