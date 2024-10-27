using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PageNationScript : MonoBehaviour
{
    public GameObject itemPrefab; // アイテムのプレハブ
    public Transform content; // ScrollViewのContent
    public Button nextButton; // 次へボタン
    public Button prevButton; // 前へボタン

    private List<string> items; // データリスト
    private int currentPage = 0; // 現在のページ
    private int itemsPerPage = 5; // 1ページあたりのアイテム数

    void Start()
    {
        // データの初期化
        items = new List<string> { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10" };
        UpdatePage();

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
    }

    void UpdatePage()
    {
        // 現在のページのアイテムを表示
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        int start = currentPage * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, items.Count);

        for (int i = start; i < end; i++)
        {
            GameObject item = Instantiate(itemPrefab, content);
            item.GetComponentInChildren<Text>().text = items[i];
        }

        // ボタンの可否を設定
        prevButton.interactable = currentPage > 0;
        nextButton.interactable = end < items.Count;
    }

    void NextPage()
    {
        if ((currentPage + 1) * itemsPerPage < items.Count)
        {
            currentPage++;
            UpdatePage();
        }
    }

    void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }
}