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
    public  string N;
    public  string bN;

    public TextMeshProUGUI name;
    public TextMeshProUGUI bookName;

    public TextMeshProUGUI inputField; // 質問を入力するフィールド
    public TextMeshProUGUI responseText; // 返答を表示するテキスト
    public Button sendButton; // 質問を送信するボタン
    public GameObject startClicked;
    public Button stopButton;//　ストップボタン
    public GameObject stopClicked;
    public Button restartButton;//　リスタートボタン
    public GameObject restartClicked;
    private bool isPlaying = false;
    public float scrollSpeed = 1.0f;
   
    private string apiKey = "";
    private string googleApiKey = "";

    public Slider audioSlider;
    public AudioSource audioSource;
    private AudioClip audioClip;


    void Start()
    {
        name.text = TopDire.writerName;
        bookName.text = TopDire.titleName;
        N = TopDire.writerName;
        bN =TopDire.titleName;
        sendButton.onClick.AddListener(OnSendButtonClicked);
        stopButton.onClick.AddListener(OnStopButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        audioSlider.onValueChanged.AddListener(OnSliderValueChanged);
        N=name.text;
        bN = bookName.text;
    }
    
    //再生ボタンのメソッド
    public void OnSendButtonClicked()
    {
       

        if (audioClip == null) //新しい音声を生成する
        {
            string prompt = N + "の" + bN + "を要約してください";
  
            StartCoroutine(SendRequest(prompt));
            isPlaying = false;
           

        }

        audioSource.UnPause();
        //audioSource.Play();
        isPlaying = false;

        startClicked.SetActive(false);
        stopClicked.SetActive(true);
    }

    //停止ボタンのメソッド
    public void OnStopButtonClicked()
    {
     
        audioSource.Pause();
        isPlaying = true; //一時停止フラグをリセット

        stopClicked.SetActive(false);
        startClicked.SetActive(true);
    }

    //リスタートボタンのメソッド
    public void OnRestartButtonClicked()
    {
        

        if(audioClip != null)
        {
            audioSource.Stop();
            audioSource.time = 0;
            audioSource.Play();
            audioSlider.value = 0;　　//スライダーをリセット
            isPlaying = false;
        }
        stopClicked.SetActive(true);
        startClicked.SetActive(false);
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
                audioClip = DownloadHandlerAudioClip.GetContent(www);
                var audioSource = GetComponent<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.Play();
                //isPlaying = true;
                audioSlider.maxValue = audioClip.length;
                audioSlider.value =0f;

                float elapsedTime = 0f;

                RectTransform rectTransform = responseText.GetComponent<RectTransform>();
                float scrollDistance = rectTransform.rect.height;

                while (audioSource.isPlaying)
                {
                    rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
                    audioSlider.value =elapsedTime;
                    yield return null;
                }
                StartCoroutine(UpdateSlider());
                rectTransform.anchoredPosition = new Vector2(0, 0);
            }
        }
        
    }
    IEnumerator UpdateSlider()
    {
        while (audioSource.isPlaying)
        {
            isPlaying = false;
            audioSlider.value = 0;
            yield return null;
        }
        isPlaying = false;
    }

    public void OnSliderValueChanged(float value)
    {
        //Debug.Log("Slider");
        if (audioSource.isPlaying)
        {
            //audioSource.time = value;
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

[System.Serializable]
public class AudioData
{
    public string audioContent;
}

