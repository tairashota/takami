using JTalkDll;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject textPrefab; //各段階のプレハブ

    public TMP_Text text;  //TextをInspectorからアタッチ

    public Slider progressSlider; //sliderをInspectorからアタッチ

    public Button playButton; //再生ボタン

    public GameObject playClickd;

    public Button pauseButton; //一時停止ボタン

    public GameObject pauseClickd;

    public Button restartButton; //リスタートボタン

    public GameObject restartClickd;

    public ScrollRect scrollRect; //ScrollrectをInspectorからアタッチ

    



    private string[] paragraphs = {
        "吾輩は猫である。",
        "名前はまだにゃい。",
        "どこで生まれたか頓（とん）と見当がつかぬ。",
        "なんでも薄暗いじめじめした所で、",
        "ニャーニャー泣いていた事だけは記憶している。",
        //必要な段落を追加
    };

    private int currentParagraphIndex = 0;

    private Coroutine displayCoroutine;

    private List<GameObject>paragraphObjects=new List<GameObject>();

    private void Start()
    {
        //最初の段落を表示
        text.text = paragraphs[currentParagraphIndex];
        playButton.onClick.AddListener(StartReading);
        pauseButton.onClick.AddListener(PauseReading);
        restartButton.onClick.AddListener(RestartReading);
        progressSlider.minValue = 0;
        progressSlider.maxValue = paragraphs.Length - 1;
        progressSlider.onValueChanged.AddListener(OnSliderValueChanged);
        text.enabled = false;

    }

    void InitiallizedParagraphs()
    {
        foreach(string paragraph in paragraphs)
        {
            GameObject paragraphObject = Instantiate(textPrefab, scrollRect.content);
            paragraphObject.GetComponent < Text >().text= "";
            paragraphObjects.Add(paragraphObject);
        }
        UpdateDisplay();
    }
    //StartボタンのOnClick()にアタッチ
    public void StartReading()
    {
        text.enabled= true;
        playClickd.SetActive(false);
        pauseClickd.SetActive(true);
        if (displayCoroutine == null) return;
        displayCoroutine = StartCoroutine(DisplayParagraphs());
    }

    //StopボタンのOnCick()にアタッチ
    public void PauseReading()
    {
        pauseClickd.SetActive(false);
        playClickd.SetActive(true);
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }
    }

    //RestartボタンのOnClick()にアタッチ
    public void RestartReading()
    {
        pauseClickd.SetActive(true);
        if (true)
        {
            
        }
        PauseReading();
        currentParagraphIndex = 0;
        text.text = paragraphs[currentParagraphIndex];
        UpdateDisplay();
    }

    private IEnumerator DisplayParagraphs()
    {
        while (currentParagraphIndex < paragraphs.Length)
        {
            text.text = paragraphs[currentParagraphIndex];
            //progressSlider.value = currentParagraphIndex;

            yield return StartCoroutine(DisplayText(paragraphs[currentParagraphIndex]));
            //スクロール位置を更新
            scrollRect.verticalNormalizedPosition = 1;

            //ここで段落を表示する時間を設定
            yield return new WaitForSeconds(2f);

            currentParagraphIndex++;
        }

        //読み終わったら一時停止
        PauseReading();

    }

    IEnumerator DisplayText(string text)
    {
        Text paragraphText = paragraphObjects[currentParagraphIndex].GetComponent<Text>();
        paragraphText.text = "";

        foreach(char letter in text)
        {
            paragraphText.text+= letter;
            yield return new WaitForSeconds(0.1f);
        }
        ScrollToCurrentParagraph();
    }

    void UpdateDisplay()
    {
        /*text.text += paragraphs[currentParagraphIndex];
        progressSlider.value = currentParagraphIndex;
        ScrollToCurrentParagraph();*/
        for(int i = 0; i < paragraphObjects.Count; i++)
        {
            paragraphObjects[i].SetActive(i == currentParagraphIndex);
        }
        progressSlider.value = currentParagraphIndex;
        ScrollToCurrentParagraph();
    }

    void ScrollToCurrentParagraph()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f - (float)currentParagraphIndex / (paragraphs.Length - 1);
    }

    private void OnSliderValueChanged(float value)
    {
        currentParagraphIndex=Mathf.FloorToInt(value);
        UpdateDisplay();

        /*//現在の段落に合わせてスクロール位置を更新
        scrollRect.verticalNormalizedPosition = 1;

        //現在の段落に合わせてコルーチンを一時停止
        if(displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine=StartCoroutine(DisplayParagraphs());
        }*/

    }

    string GenerateSummary(string paragraph)
    {
        string[] sentences=paragraph.Split('。');
        return sentences.Length > 0 ? sentences[0] + "。" : "";
    }
}
