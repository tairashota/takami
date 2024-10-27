using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoCorrectClick : MonoBehaviour
{
    public void serachResultScene()
    {
        var autoCorrectC = GetChildren(this.gameObject);
        //検索欄が空でなければ
        if (SearchEngineDire.keywards != "")
        {
            //著者名でCSVから読み込んだリストに対して検索
            if (SearchEngineDire.searchDestination == 0)
            {
                foreach (var item in NovelDataLoadCSV.novels)
                {
                    if (item.name.StartsWith(autoCorrectC[0].GetComponent<TextMeshProUGUI>().text) || item.nameSort.StartsWith(autoCorrectC[0].GetComponent<TextMeshProUGUI>().text))
                    {
                        SearchEngineDire.serachResult serachResult = new SearchEngineDire.serachResult();
                        serachResult.bookWriters = item.name;
                        serachResult.bookTitle = item.title;

                        //検索結果リストへ追加
                        SearchEngineDire.serachResultsList.Add(serachResult);
                    }
                }
            }
            //作品名でCSVから読み込んだリストに対して検索
            else if (SearchEngineDire.searchDestination == 1)
            {
                foreach (var item in NovelDataLoadCSV.novels)
                {
                    if (item.title.StartsWith(autoCorrectC[0].GetComponent<TextMeshProUGUI>().text) || item.titleSort.StartsWith(autoCorrectC[0].GetComponent<TextMeshProUGUI>().text))
                    {

                        SearchEngineDire.serachResult serachResult = new SearchEngineDire.serachResult();
                        serachResult.bookWriters = item.name;
                        serachResult.bookTitle = item.title;

                        //検索結果リストへ追加
                        SearchEngineDire.serachResultsList.Add(serachResult);
                    }
                }
            }
            SearchEngineDire.resultW = autoCorrectC[0].GetComponent<TextMeshProUGUI>().text;
            FreeSearchEnginDire.n = 0;
            SceneManager.LoadScene("AuthorScene");
        }
        
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
}
