using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using TMPro;

public class ChatGPTExample : MonoBehaviour
{
    public static string name = "";
    public static string bookName = "";
    public TextMeshProUGUI responseText; // 返答を表示するテキスト
    public Button sendButton; // 質問を送信するボタン
    private string apiKey = "sk-yofwMBW17lZMrK2csBRZcftxMndBVcL9phvBjBUH8VT3BlbkFJ5W1Szit2hx7zBxq4iwuq9jNM3CGbVYBRI0t0phS1cA";
    private string googleApiKey = "AIzaSyCcRfZrotPW2pmzeWQuy7FhVmYKzHftejs";

    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
    }

    void OnSendButtonClicked()
    {
        string template = string.Format("{0}の{1}を要約してください", name, bookName);
        StartCoroutine(SendRequest(template));
    }

    IEnumerator SendRequest(string prompt)
    {
        var request = new UnityWebRequest("https://api.openai.com/v1/chat/completions", "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        var json = new
        {
            model = "gpt-3.5-turbo",
            messages = new[] { new { role = "user", content = prompt } }
        };
        string jsonBody = JsonConvert.SerializeObject(json);
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            responseText.text = "エラーが発生しました: " + request.error;
        }
        else
        {
            var response = JsonConvert.DeserializeObject<ChatCompletionResponse>(request.downloadHandler.text);
            string chatResponse = response.choices[0].message.content;
            responseText.text = chatResponse;
            // 返答を音声で読み上げる
            StartCoroutine(SynthesizeSpeech(chatResponse));
        }
    }

    IEnumerator SynthesizeSpeech(string text)
    {
        string url = "https://texttospeech.googleapis.com/v1/text:synthesize?key=" + googleApiKey;
        var json = new
        {
            input = new { text = text },
            voice = new { languageCode = "ja-JP", ssmlGender = "FEMALE" },
            audioConfig = new { audioEncoding = "MP3" }
        };
        string jsonBody = JsonConvert.SerializeObject(json);
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            var response = JsonConvert.DeserializeObject<GoogleTTSResponse>(request.downloadHandler.text);
            byte[] audioData = System.Convert.FromBase64String(response.audioContent);
            string path = Path.Combine(Application.persistentDataPath, "speech.mp3");
            File.WriteAllBytes(path, audioData);
            StartCoroutine(PlayAudio(path));
        }
    }

    IEnumerator PlayAudio(string path)
    {
        using (var www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                var clip = DownloadHandlerAudioClip.GetContent(www);
                var audioSource = GetComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
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

public class GoogleTTSResponse
{
    public string audioContent { get; set; }
}
