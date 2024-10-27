using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScrollController : MonoBehaviour
{

    public GameObject itemPrefab; // 作品のプレハブ
    public Sprite[] images;        // 画像を格納する配列
    public RectTransform content;  // Scroll RectのContent部分

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
