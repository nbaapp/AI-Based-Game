using System.Collections.Generic;
using OpenAI;
using UnityEngine;

public class NPC : Character
{
    public string goals;
    public string personality;
    public string physicalDescription;
    private bool hasMetPlayer = false;
    public List<ChatMessage> interactionHistory = new List<ChatMessage>();
    public List<ChatMessage> battleManager = new List<ChatMessage>();

    [TextArea(15, 20)] public string battleSystemPrompt =
    @"You are a character in a game. You will have interactions with the player character, who can say something friendly,";
    [TextArea(15,20)] public string interactionSystemPrompt =
    @"You are a character in a game. You will have interactions with the player character, who can say something friendly,
    neutral, or rude to you. When you respond, you will not respond with a sentence, but with a single word describing what
    you are feeling, chosen from a set of options. You will not be able to add any additional information to your response.

    Unless you are going to battle, Your response must follow this format: Response: [response]
    The response must be a single word chosen from the following options:
    ""Friendly""
    ""Neutral""
    ""Rude""
    
    Your response must be one of these options, and you may not respond with anything else, or add any additional information in your response.
    Your responses should reflect how the character would feel in response to the player's actions. For example, if the player acts rude to you,
    you might initially be inclided to be polite, but if repeatedly provoked, you might become rude in response. You also might be reluctant to
    be friendly again to that player. do to their actions.
    
    Example interaction:
    
    Input: The player acted rude
    
    Response: neutral
    
    If the input is ""You have entered a battle";


    private void Start()
    {

        interactionHistory.Add(new ChatMessage()
        {
            Role = "system",
            Content = interactionSystemPrompt
        });
        /*
        interactionHistory.Add(new ChatMessage()
        {
            Role = "system",
            Content = "Here is the information about your character \n" + PrintData()
        }); */
    }

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
