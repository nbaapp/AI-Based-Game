using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    public GameObject PlayerUI;
    private PlayerCombatant player;

    void Start()
    {
        player = FindObjectOfType<PlayerCombatant>();
    }

    public void ShowPlayerUI()
    {
        PlayerUI.SetActive(true);
    }

    public void HidePlayerUI()
    {
        PlayerUI.SetActive(false);
    }

    public void TakeAction(int actionIndex)
    {
        player.Act();
    }
}
