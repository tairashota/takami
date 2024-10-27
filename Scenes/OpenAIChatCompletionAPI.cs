using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; // JSONライブラリを使用

public class OpenAIChatCompletionAPI : MonoBehaviour
{
    private string apiKey = "sk-yofwMBW17lZMrK2csBRZcftxMndBVcL9phvBjBUH8VT3BlbkFJ5W1Szit2hx7zBxq4iwuq9jNM3CGbVYBRI0t0phS1cA";
    private string googleApiKey = "AIzaSyCcRfZrotPW2pmzeWQuy7FhVmYKzHftejs";
    const string API_URL = "sk-yofwMBW17lZMrK2csBRZcftxMndBVcL9phvBjBUH8VT3BlbkFJ5W1Szit2hx7zBxq4iwuq9jNM3CGbVYBRI0t0phS1cA";
  
    JsonSerializerSettings settings = new JsonSerializerSettings();

    public OpenAIChatCompletionAPI(string apiKey)
    {
        this.apiKey = apiKey;
    }

    IEnumerator SendRequest(string prompt)
    {
        var request = new UnityWebRequest(API_URL, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        var json = new
        {
            model = "gpt-3.5-turbo",
            messages = new[] { new { role = "user", content = prompt } },
            max_tokens = 5
        };

        var jsonBody = JsonConvert.SerializeObject(json, settings);
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            var response = JsonConvert.DeserializeObject<ChatCompletionResponse>(request.downloadHandler.text);
            Debug.Log(response.choices[0].message.content);
        }
    }
    public class ChatCompletionResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
