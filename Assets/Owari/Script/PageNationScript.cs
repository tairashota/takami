using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PageNationScript : MonoBehaviour
{
    public GameObject itemPrefab; // �A�C�e���̃v���n�u
    public Transform content; // ScrollView��Content
    public Button nextButton; // ���փ{�^��
    public Button prevButton; // �O�փ{�^��

    private List<string> items; // �f�[�^���X�g
    private int currentPage = 0; // ���݂̃y�[�W
    private int itemsPerPage = 5; // 1�y�[�W������̃A�C�e����

    void Start()
    {
        // �f�[�^�̏�����
        items = new List<string> { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10" };
        UpdatePage();

        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
    }

    void UpdatePage()
    {
        // ���݂̃y�[�W�̃A�C�e����\��
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

        // �{�^���̉ۂ�ݒ�
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