using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImageController : MonoBehaviour
{
    public Image targetImage; // ここにImageコンポーネントをアサイン
    public Sprite[] images;   // 画像を格納する配列

    // private ImageConversion image;

    // Start is called before the first frame update
    void Start()
    {
        if (images.Length > 0 && targetImage != null)
        {
            // ランダムなインデックスを生成
            int randomIndex = Random.Range(0, images.Length);
            // ランダムに選んだ画像を設定
            targetImage.sprite = images[randomIndex];
        }
        else
        {
            Debug.LogError("Images array is empty or targetImage is not assigned.");
        }

    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
