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
    public GameObject textPrefab; //�e�i�K�̃v���n�u

    public TMP_Text text;  //Text��Inspector����A�^�b�`

    public Slider progressSlider; //slider��Inspector����A�^�b�`

    public Button playButton; //�Đ��{�^��

    public GameObject playClickd;

    public Button pauseButton; //�ꎞ��~�{�^��

    public GameObject pauseClickd;

    public Button restartButton; //���X�^�[�g�{�^��

    public GameObject restartClickd;

    public ScrollRect scrollRect; //Scrollrect��Inspector����A�^�b�`

    



    private string[] paragraphs = {
        "��y�͔L�ł���B",
        "���O�͂܂��ɂႢ�B",
        "�ǂ��Ő��܂ꂽ���ځi�Ƃ�j�ƌ��������ʁB",
        "�Ȃ�ł����Â����߂��߂������ŁA",
        "�j���[�j���[�����Ă����������͋L�����Ă���B",
        //�K�v�Ȓi����ǉ�
    };

    private int currentParagraphIndex = 0;

    private Coroutine displayCoroutine;

    private List<GameObject>paragraphObjects=new List<GameObject>();

    private void Start()
    {
        //�ŏ��̒i����\��
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
    //Start�{�^����OnClick()�ɃA�^�b�`
    public void StartReading()
    {
        text.enabled= true;
        playClickd.SetActive(false);
        pauseClickd.SetActive(true);
        if (displayCoroutine == null) return;
        displayCoroutine = StartCoroutine(DisplayParagraphs());
    }

    //Stop�{�^����OnCick()�ɃA�^�b�`
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

    //Restart�{�^����OnClick()�ɃA�^�b�`
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
            //�X�N���[���ʒu���X�V
            scrollRect.verticalNormalizedPosition = 1;

            //�����Œi����\�����鎞�Ԃ�ݒ�
            yield return new WaitForSeconds(2f);

            currentParagraphIndex++;
        }

        //�ǂݏI�������ꎞ��~
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

        /*//���݂̒i���ɍ��킹�ăX�N���[���ʒu���X�V
        scrollRect.verticalNormalizedPosition = 1;

        //���݂̒i���ɍ��킹�ăR���[�`�����ꎞ��~
        if(displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine=StartCoroutine(DisplayParagraphs());
        }*/

    }

    string GenerateSummary(string paragraph)
    {
        string[] sentences=paragraph.Split('�B');
        return sentences.Length > 0 ? sentences[0] + "�B" : "";
    }
}
