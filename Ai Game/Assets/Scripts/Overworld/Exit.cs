using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Direction direction;
    public Scenario destination;
    public bool locked = false;

    public Exit(Direction dir, Scenario dest, bool lockStatus)
    {
        direction = dir;
        destination = dest;
        locked = lockStatus;
    }
}
