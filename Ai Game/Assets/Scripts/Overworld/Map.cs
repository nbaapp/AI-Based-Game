using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int length;
    public int width;
    public Scenario[,] scenarios;
    Logic logic;

    void Start()
    {
        logic = FindObjectOfType<Logic>();
        
        //fill with locations
        scenarios = new Scenario[length, width];
    }

    //fill with locations
    public Map(int l, int w)
    {
        length = l;
        width = w;
        scenarios = new Scenario[l, w];
        for (int i = 0; i < l; i++)
        {
            for (int j = 0; j < w; j++)
            {
                scenarios[i, j] = logic.GetScenarioFromCoordinates(i, j);
            }
        }
    }

    public void AddLocation(Scenario scenario, int x, int y)
    {
        scenarios[x, y] = scenario;
    }

    public void RemoveLocation(int x, int y)
    {
        scenarios[x, y] = null;
    }

    public Scenario GetLocation(int x, int y)
    {
        return scenarios[x, y];
    }
}
