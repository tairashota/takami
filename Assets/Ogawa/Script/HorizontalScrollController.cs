using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScrollController : MonoBehaviour
{

    public GameObject itemPrefab; // ��i�̃v���n�u
    public Sprite[] images;        // �摜���i�[����z��
    public RectTransform content;  // Scroll Rect��Content����

    // Start is called before the first frame update
    void Start()
    {
        CreateItems();
    }

    void CreateItems()
    {
        foreach (Sprite image in images)
        {
            GameObject item = Instantiate(itemPrefab, content);
           // Image img = item.GetComponent<Image>();
           // img.sprite = image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
