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

    [TextArea(15,20)] public string agentSystem =
    @"You are an assistant hepling to set up a game. You will be given information about the world of the game. Your will then create a
    number of characters in that game by listing several attributes: name, physical description, personal goals, and personality. Keep your
    answers to each question at 100 words or fewer. Each time you are given the prompt ""New Character"", you will create a new character.
    
    Format your response as follows:

    Name: [name]
    Physical Description: [description]
    Personal Goals: [goals]
    Personality: [personality]";


    private List<ChatMessage> agentThread = new List<ChatMessage>();
    public AIGameBackground worldInfo;

    //list of npcs of size logic.numberOfNPCs
    public GameObject npcPrefab;
    public List<NPC> npcs;
    public UnityEvent DoneWithSetup = new UnityEvent();
    public UnityEvent<string> ResponseRecieved = new UnityEvent<string>();

    private async void Start()
    {
        openAI = FindObjectOfType<OpenAIController>();
        logic = FindObjectOfType<Logic>();

        agentThread.Add(new ChatMessage()
        {
            Role = "system",
            Content = agentSystem
        });
        agentThread.Add(new ChatMessage()
        {
            Role = "system",
            Content = "Here is the information about the world you will be creating characters for: \n" + worldInfo.PrintData()
        });
        
        await AISetup();
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


}
