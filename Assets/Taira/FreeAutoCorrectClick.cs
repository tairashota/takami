using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeAutoCorrectClick : MonoBehaviour
{

    GameObject Topdire;
    public resultGenerator resultGenerator;

    private void Start()
    {
        Topdire = GameObject.Find("ResultGenerator");
    }
    private static GameObject[] GetChildren(GameObject parent)
    {
        // 親オブジェクトのTransformを取得
        var parentTransform = parent.transform;

        // 子オブジェクトを格納する配列作成
        var children = new GameObject[parentTransform.childCount];

        // 0～個数-1までの子を順番に配列に格納
        for (var i = 0; i < children.Length; ++i)
        {
            // Transformからゲームオブジェクトを取得して格納
            children[i] = parentTransform.GetChild(i).gameObject;
        }

        // 子オブジェクトが格納された配列
        return children;
    }

    public void serachResultScene()
    {

            FreeSearchEnginDire.serachResultDatas = new List<SearchEngineDire.serachResult>();
        foreach (var item in SearchEngineDire.serachResultsList)
        {
            if (item.bookWriters.Contains(FreeSearchEnginDire.keywards))
            {
                SearchEngineDire.serachResult serachResult = new SearchEngineDire.serachResult();
                serachResult.bookWriters = item.bookWriters;
                serachResult.bookTitle = item.bookTitle;
                FreeSearchEnginDire.serachResultDatas.Add(serachResult);
            }
            if (item.bookTitle.Contains(FreeSearchEnginDire.keywards))
            {
                SearchEngineDire.serachResult serachResult = new SearchEngineDire.serachResult();
                serachResult.bookWriters = item.bookWriters;
                serachResult.bookTitle = item.bookTitle;
                FreeSearchEnginDire.serachResultDatas.Add(serachResult);
            }
            Debug.Log(item.bookTitle);
        }

        SceneManager.LoadScene("AuthorScene");
    }
}
