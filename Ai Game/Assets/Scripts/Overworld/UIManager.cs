using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Logic logic;
    public TextMeshProUGUI AIText;
    public GameObject dialogueBox, mainMenu, talkMenu;
    private NPC currentNPC;
    private AIManager aiManager;

    public void Start()
    {
        logic = FindObjectOfType<Logic>();
        aiManager = FindObjectOfType<AIManager>();

        dialogueBox.SetActive(false);
        mainMenu.SetActive(false);
        talkMenu.SetActive(false);
    }

    public void SetCurrentNPC(NPC npc)
    {
        currentNPC = npc;
    }

    public void SetAIText(string text)
    {
        AIText.text = text;
    }

    public void OpenDialogueBox()
    {
        if (currentNPC == null)
        {
            Debug.LogWarning("No current NPC set");
            return;
        }
        logic.DisablePlayerControls();
        dialogueBox.SetActive(true);
        mainMenu.SetActive(true);
    }

    private void CloseDialogueBox()
    {
        logic.EnablePlayerControls();
        dialogueBox.SetActive(false);
        mainMenu.SetActive(true);
        currentNPC = null;
    }

    public void ExitMainMenuButton()
    {
        mainMenu.SetActive(false);
        CloseDialogueBox();
    }

    public void TalkButton()
    {
        mainMenu.SetActive(false);
        talkMenu.SetActive(true);
    }

    public void ExitTalkMenuButton()
    {
        talkMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public async void FriendlyButton()
    {
        Debug.Log("Said something friendly");
        currentNPC.SetMetPlayer(true);
        string response = await aiManager.SendMessageToThread("Player acted friendly", currentNPC.interactionHistory);
        SetAIText(response);
    }

    public async void NeutralButton()
    {
        Debug.Log("Said something neutral");
        currentNPC.SetMetPlayer(true);
        string response = await aiManager.SendMessageToThread("Player acted neutral", currentNPC.interactionHistory);
        SetAIText(response);
    }

    public async void RudeButton()
    {
        Debug.Log("Said something rude");
        currentNPC.SetMetPlayer(true);
        string response = await aiManager.SendMessageToThread("Player acted rude", currentNPC.interactionHistory);
        SetAIText(response);
    }

    public void ChallengeButton()
    {
        Debug.Log("Challenged the NPC");
    }
}
