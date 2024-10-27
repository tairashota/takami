using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Image Musimegane2; // 切り替えるImageオブジェクト
    public Sprite[] images; // 切り替えたい画像の配列
    private int currentIndex = 0; // 現在の画像のインデックス

    void Start()
    {
        // 最初の画像を設定
        if (images.Length > 0)
        {
            Musimegane2.sprite = images[currentIndex];
        }
    }

    public void SwitchImage()
    {
        // インデックスを更新
        currentIndex = (currentIndex + 1) % images.Length; // 次の画像に移動（ループ）

        // 画像を切り替える
        Musimegane2.sprite = images[currentIndex];
    }
}
    

