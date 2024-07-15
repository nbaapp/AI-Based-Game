using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;
using System.Linq;
using System.Threading.Tasks;
public class AIManager : MonoBehaviour
{
    
    private OpenAIController openAI;
    private Logic logic;

    [TextArea(15,20)] public string storySystem =
    @"You a storyteller in a game. You are provided with basic data about the world that the game takes place in, then
    you will get very basic scenarios that the player enters, and you will flesh out the scenarios with more detail
    relevant to the storyline. In addition, you will recieve details about other characters that may have entered the
    scenario dynamically. Integrate these into the story as well. The only thing you should not direct is the player's actions.
    When you have reached a point where the player can act, pause and wait for more input from the player. Limit your responses to 150 words or fewer.";

    [TextArea(15,20)] public string prepSystem =
    @"You are a helper assisitng to set up a game. You will be asked to provide a setting for the game. You will be asked for
    several atributes of the world: name of the world, current state of the world, starting location of the player, and a brief
    description of the world. Keep your responses to each question at 100 words or fewer. Keep the theme of the world in the realm
    of fantasy, so that typical fantasy game mechanics make sense. Keep your response to each question at 100 words or fewer. Each time
    you are given the prompt ""New World"", you will create a new world.
    
    Format your responses as follows:
    
    Name: [name]
    State: [state]
    Starting Location: [location]
    Description: [description]";

    [TextArea(15,20)] public string agentSystem =
    @"You are an assistant hepling to set up a game. You will be given information about the world of the game. Your will then create a
    number of characters in that game by listing several attributes: name, physical description, personal goals, and personality. Keep your
    answers to each question at 100 words or fewer. Each time you are given the prompt ""New Character"", you will create a new character.
    
    Format your response as follows:

    Name: [name]
    Physical Description: [description]
    Personal Goals: [goals]
    Personality: [personality]";

    public List<ChatMessage> storyThread = new List<ChatMessage>();

    private List<ChatMessage> prepThread = new List<ChatMessage>();

    private List<ChatMessage> agentThread = new List<ChatMessage>();
    public WorldInfo worldInfo;

    //list of npcs of size logic.numberOfNPCs
    public GameObject npcPrefab;
    public List<NPC> npcs;
    public UnityEvent DoneWithSetup = new UnityEvent();
    public UnityEvent<string> ResponseRecieved = new UnityEvent<string>();

    private async void Start()
    {
        openAI = FindObjectOfType<OpenAIController>();
        logic = FindObjectOfType<Logic>();

        prepThread.Add(new ChatMessage()
        {
            Role = "system",
            Content = prepSystem
        });
        agentThread.Add(new ChatMessage()
        {
            Role = "system",
            Content = agentSystem
        });
        storyThread.Add(new ChatMessage()
        {
            Role = "system",
            Content = storySystem
        });

        await AISetup();
    }

    public ChatMessage GetStoryResponse()
    {
        return storyThread.Last();
    }

    public async Task<string> SendMessageToThread(string message, List<ChatMessage> thread)
    {
        var tcs = new TaskCompletionSource<string>();

        void OnMessageReceived(string response)
        {
            openAI.ResponseReceived.RemoveListener(OnMessageReceived);
            tcs.SetResult(response);
        }

        openAI.ResponseReceived.AddListener(OnMessageReceived);
        openAI.Send(thread, message);

        return await tcs.Task;
    }

    private async Task AISetup()
    {
        var prepResponse = await SendMessageToThread("New World", prepThread);
        if (prepResponse != null)
        {
            ParsePrepResponse(prepResponse);
        }

        storyThread.Add(new ChatMessage()
        {
            Role = "system",
            Content = "Here is the info for the world of the game:" + "\n" + worldInfo.PrintData()
        });

        for (int i = 0; i < logic.numberOfNPCs; i++)
        {
            var agentResponse = await SendMessageToThread("New Character", agentThread);
            if (agentResponse != null)
            {
                NPC newNPC = Instantiate(npcPrefab, Vector3.zero, Quaternion.identity).GetComponent<NPC>();
                npcs.Add(newNPC);
                ParseAgentResponse(agentResponse, npcs[i]);
                newNPC.name = newNPC.characterName;
            }
        }

        DoneWithSetup.Invoke();
    }


    private void ParseAgentResponse(string response, NPC npc)
    {
        string[] splitResponse = response.Split('\n');

        foreach (string line in splitResponse)
        {
            if (line.Contains("Name:"))
            {
                npc.characterName = line.Replace("Name: ", "");
            }
            else if (line.Contains("Physical Description:"))
            {
                npc.physicalDescription = line.Replace("Physical Description: ", "");
            }
            else if (line.Contains("Personal Goals:"))
            {
                npc.goals = line.Replace("Personal Goals: ", "");
            }
            else if (line.Contains("Personality:"))
            {
                npc.personality = line.Replace("Personality: ", "");
            }
        }
    }

    private void ParsePrepResponse(string response)
    {
        string[] splitResponse = response.Split('\n');

        foreach (string line in splitResponse)
        {
            if (line.Contains("Name:"))
            {
                worldInfo.worldName = line.Replace("Name: ", "");
            }
            else if (line.Contains("State:"))
            {
                worldInfo.worldState = line.Replace("State: ", "");
            }
            else if (line.Contains("Starting Location:"))
            {
                worldInfo.startLocation = line.Replace("Starting Location: ", "");
            }
            else if (line.Contains("Description:"))
            {
                worldInfo.description = line.Replace("Description: ", "");
            }
        }
    }


}
