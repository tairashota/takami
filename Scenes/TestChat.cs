using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;

public class TestChat : MonoBehaviour
{
    public static string gameObject;

    #region 必要なクラスの定義など
    [System.Serializable]
    public class MessageModel
    {
        public string role;
        public string content;
    }
    [System.Serializable]
    public class CompletionRequestModel
    {
        public string model;
        public List<MessageModel> messages;
    }

    [System.Serializable]
    public class ChatGPTRecieveModel
    {
        public string id;
        public string @object;
        public int created;
        public Choice[] choices;
        public Usage usage;

        [System.Serializable]
        public class Choice
        {
            public int index;
            public MessageModel message;
            public string finish_reason;
        }

        [System.Serializable]
        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }
    #endregion

    private MessageModel assistantModel = new()
    {
        role = "system",
        content = "あなたは小説を要約するAIです"
    };
    private readonly string apiKey = "sk-yofwMBW17lZMrK2csBRZcftxMndBVcL9phvBjBUH8VT3BlbkFJ5W1Szit2hx7zBxq4iwuq9jNM3CGbVYBRI0t0phS1cA";
    private List<MessageModel> communicationHistory = new();

    void Start()
    {
        communicationHistory.Add(assistantModel);
        MessageSubmit("夏目漱石のこころを要約してください");
    }
    //newMessageは自分の言葉
    private void Communication(string newMessage, Action<MessageModel> result)
    {
        Debug.Log(newMessage);
        communicationHistory.Add(new MessageModel()
        {
            role = "user",
            content = newMessage

        });

        var apiUrl = "https://api.openai.com/v1/chat/completions";
        var jsonOptions = JsonUtility.ToJson(
            new CompletionRequestModel()
            {
                model = "gpt-3.5-turbo",
                messages = communicationHistory
            }, true);
        var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + apiKey},
                {"Content-type", "application/json"},
                {"X-Slack-No-Retry", "1"}
            };
        var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        var operation = request.SendWebRequest();

        operation.completed += _ =>
        {
            if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
                       operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(operation.webRequest.error);
                throw new Exception();
            }
            else
            {
                var responseString = operation.webRequest.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<ChatGPTRecieveModel>(responseString);
                communicationHistory.Add(responseObject.choices[0].message);
                Debug.Log(responseObject.choices[0].message.content);
                
               
            }
            request.Dispose();

        };
    }

    public void MessageSubmit(string sendMessage)
    {
        Communication(sendMessage, (result) =>
        {
            Debug.Log(result.content);
        });
    }
}