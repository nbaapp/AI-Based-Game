using UnityEngine;
using System.Collections.Generic;
using OpenAI;
using UnityEngine.Events;

public class OpenAIController : MonoBehaviour
{
    private OpenAIApi openai = new OpenAIApi();

    public UnityEvent<string> ResponseReceived = new UnityEvent<string>();

    private void Start()
    {
        ResponseReceived.AddListener(OnResponseReceived);
    }
    

    public async void Send(List<ChatMessage> thread, string messageToSend, int maxTokens = 200)
    {
        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = messageToSend
        };
                
        thread.Add(newMessage);

        Debug.Log("Sending message...)");
        
        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-4o",
            Messages = thread,
            Temperature = 1f,
            MaxTokens = maxTokens,
            PresencePenalty = 0f,
            FrequencyPenalty = 0f
        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();
            
            thread.Add(message);
            //send event that respnse received
            ResponseReceived.Invoke(message.Content);
            
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }
    }

    private void OnResponseReceived(string message)
    {
        Debug.Log("Message received: " + message);
    }
}

