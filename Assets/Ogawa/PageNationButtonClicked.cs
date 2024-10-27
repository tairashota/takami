using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PageNationButtonClicked : MonoBehaviour
{

    public Button[] buttons; // �Ǘ�����{�^���̔z��
    private Button activeButton; // ���݃A�N�e�B�u�ȃ{�^��
    private Color originalColor; // ���̐F

    // Start is called before the first frame update
    void Start()
    {
        // �{�^���̏����ݒ�
        foreach (Button button in buttons)
        {
            ColorBlock cb = button.colors;
            cb.normalColor = button.GetComponent<Image>().color;
            button.colors = cb;
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    void OnButtonClick(Button clickedButton)
    {
        // �ȑO�̃A�N�e�B�u�ȃ{�^��������Ό��ɖ߂�
        if (activeButton != null && activeButton != clickedButton)
        {
            ColorBlock cb = activeButton.colors;
            cb.normalColor = originalColor;
            activeButton.colors = cb;
        }

        // ���݂̃{�^�����A�N�e�B�u�ɂ���
        activeButton = clickedButton;
        originalColor = clickedButton.GetComponent<Image>().color;

        // �F��Z������
        Color darkerColor = originalColor * 0.5f;
        ColorBlock newCb = clickedButton.colors;
        newCb.normalColor = darkerColor;
        clickedButton.colors = newCb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
