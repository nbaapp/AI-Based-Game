using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGameBackground : MonoBehaviour
{
    public string worldName = "Airela";
    [TextArea(15,20)] public string description =
    @"Airela was once a great kingdom, living in a golden age of magic and technology. However, 20 years ago, their hubris caught up to them.
    Thinking themselvs gods, the great mages of the kingdom, in trying to acheive a great feat of magic, instead caused a great cataclysm to
    fall upon the land. The kingdom was shattered, and the very fabric of magic was broken. The few survivors now travel wander the destroyed
    land, scrounging for survival. Above all, the survivors crave the Fragments, the only source of magic left since the Cataclysm. The survivors
    physically need the Fragments to survive, for people cannot live without magic. Trust in this land is scarece, and everyone is out for themselves,
    fighting for the Fragments, and fighting to survive. People will do what they must to survive, even if that meens stabbing their best friend in the back.";

    public string PrintData()
    {
        string data = "World Name: " + worldName + "\n" +
                      "World Description: " + description;
        return data;
    }
}
