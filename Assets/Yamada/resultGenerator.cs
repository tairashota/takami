using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static SearchEngineDire;

public class resultGenerator : MonoBehaviour
{


    //パネルのプレハブを入れる
    [SerializeField] GameObject panelPrefab;

    [SerializeField] Transform transformParent;
    //パネルの生成数(ページ数)
    public static int panelCount;

    public GameObject cav;

    int pageCount = 0;

    int bookCount = 0;

    int panelPosition = 0;

    string[] paoj;

    //ボタンのプレハブを入れる
    [SerializeField] GameObject buttonPrefab;
    //ボタンの親になる位置を入れる
    [SerializeField] Transform buttonTransform;

    GameObject lastPanel;

    public void resultGenerators(List<serachResult> serachResult)
    {
        pageCount = 0;
        bookCount = 0;
        panelPosition = 0;

        // 生成する本数に基づいて必要なパネル数を計算
        panelCount = (serachResult.Count / 9) + 1;
        // 必要な数だけパネルを生成
        for (int i = 0; i < panelCount; i++)
        {
            // パネルを生成して親オブジェクトの子として配置
            GameObject newPanel = Instantiate(panelPrefab, transformParent);
            newPanel.transform.position = new Vector3(newPanel.transform.position.x + 345.51f * i, newPanel.transform.position.y, newPanel.transform.position.z);
            newPanel.name = "Panel" + i.ToString();
            lastPanel = newPanel;
        }

        foreach (var r in serachResult)
        {
            

            int n = pageCount / 9;
            string k = "Panel" + n.ToString();

            Transform panelPage = cav.transform.Find(k);
            Debug.Log(k);


            //ページ(親)の中にある本（子）を取得
            var bookC = GetChildren(panelPage.gameObject);
            
            bookC[bookCount].GetComponent<BookCiickedController>().title = r.bookTitle;
            bookC[bookCount].GetComponent<BookCiickedController>().writer = r.bookWriters;
            Debug.Log(bookC[bookCount].GetComponent<BookCiickedController>().title);
            bookC[bookCount].SetActive(true);

            //本(親)の中にある情報(子)を取得
            var bookCC = GetChildren(bookC[bookCount]);

            bookCC[0].GetComponent<TextMeshProUGUI>().text = r.bookWriters;
            bookCC[1].GetComponent<TextMeshProUGUI>().text = r.bookTitle;
            Debug.Log(bookCC[0].GetComponentInChildren<TextMeshProUGUI>().text);

            pageCount++;
            bookCount++;
            if (bookCount == 9)
            {
                bookCount = 0;
            }
           
        }
        var lastbookC = GetChildren(lastPanel);
        //本(親)の中にある情報(子)を取得
        var lastbookCC = GetChildren(lastbookC[0]);
        Debug.Log(lastbookCC[0].GetComponent<TextMeshProUGUI>().text);
        Debug.Log(lastbookCC[0].activeSelf);
        if (lastbookCC[0].GetComponent<TextMeshProUGUI>().text == "著者名")
        {
            Debug.Log("こわした");
            Destroy(lastPanel);
        }
    }

    public void buttonGenerator(int count)
    {
        for (int i = 0; i < count; i++)
        {
            //ボタンのインスタンスを生成
            GameObject button = Instantiate(buttonPrefab, buttonTransform);
        }
    }


    // parent直下の子オブジェクトをforループで取得する
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
