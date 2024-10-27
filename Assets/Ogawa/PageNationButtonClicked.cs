using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PageNationButtonClicked : MonoBehaviour
{

    public Button[] buttons; // 管理するボタンの配列
    private Button activeButton; // 現在アクティブなボタン
    private Color originalColor; // 元の色

    // Start is called before the first frame update
    void Start()
    {
        // ボタンの初期設定
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
        // 以前のアクティブなボタンがあれば元に戻す
        if (activeButton != null && activeButton != clickedButton)
        {
            ColorBlock cb = activeButton.colors;
            cb.normalColor = originalColor;
            activeButton.colors = cb;
        }

        // 現在のボタンをアクティブにする
        activeButton = clickedButton;
        originalColor = clickedButton.GetComponent<Image>().color;

        // 色を濃くする
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
