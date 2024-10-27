using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FontDirector : MonoBehaviour
{
    public ScrollRect scrollRect;

    [SerializeField] private TMP_Text text;

    public Image gaugeFill;

    public GameObject gaugePrefab;

    public GameObject stopClicked;

    public GameObject startClicked;

    public GameObject resumeClicked;
    
    public float scrollSpeed = 0.5f;

    public float displayInterval = 0.1f;

    private string[] sentences;

    private int currentSentenceIndex = 0;

    private Coroutine displayCoroutine;

    GameObject TB;

    void Start()
    {
        sentences = new string[] { "吾輩は猫である、", "名前はまだにゃい。", "どこで生まれたか頓（とん）と見当がつかぬ。", "なんでも薄暗いじめじめした所で、ニャーニャー泣いていた事だけは記憶している。" };
        UpdateGauge();
        TB = GameObject.Find("TopButtom");

    }

    void Update()
    {
        scrollRect.verticalNormalizedPosition-=scrollSpeed*Time.deltaTime;

        if (scrollRect.verticalNormalizedPosition < 0)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
        startClicked.SetActive(true);
        stopClicked.SetActive(false);

    }

    private IEnumerator DisplaySentence(string sentence)
    {
        text.text = "";

        foreach(char letter in sentence)
        {
            text.text += letter;
            scrollRect.verticalNormalizedPosition = 1f;
            yield return new WaitForSeconds(displayInterval);
        }

        currentSentenceIndex++;

        if (currentSentenceIndex < sentences.Length)
        {
            //text.text=sentences[currentSentenceIndex];
            //currentSentenceIndex++;

            yield return new WaitForSeconds(1f);
            //scrollRect.verticalNormalizedPosition = 1f;
            displayCoroutine = StartCoroutine(DisplaySentence(sentences[currentSentenceIndex]));

            yield return new WaitForSeconds(displayInterval);
        }
        else
        {
            currentSentenceIndex = 0;
        }
    }

    public void StartDisplaying()
    {
        startClicked.SetActive(false);
        
        if (displayCoroutine == null)
        {
            displayCoroutine = StartCoroutine(DisplaySentence(sentences[currentSentenceIndex]));
        }
    }

    public void StopDisplaying()
    {
        stopClicked.SetActive(true);
        startClicked.SetActive(false);
        if(displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;

        }
    }

    public void ResumeDisplaying()
    {
        stopClicked.SetActive(false);
        stopClicked.SetActive(true);
        StartDisplaying();
    }

    private void UpdateGauge()
    {
        foreach(Transform child in gaugeFill.transform)
        {
            Destroy(child.gameObject);
        }

        for(int i=0; i < sentences.Length; i++)
        {
            GameObject gaugeItem = Instantiate(gaugePrefab, gaugeFill.transform);
            gaugeItem.GetComponentInChildren<Text>().text = (i + 1).ToString();
            Button button=gaugeItem.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => JumpToSentence(index));
        }
    }

    private void JumpToSentence(int index)
    {
        StopDisplaying();
        currentSentenceIndex = index;
        text.text = "";
        StartDisplaying();
    }

    public void OnTop()
    {
        SceneManager.LoadScene("AuthorScene");
    }
}
