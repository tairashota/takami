using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BookPrefabGenerator : MonoBehaviour
{
    //本のプレハブを入れる
    GameObject bookPrefab;

    //パネルの親を入れる
    Transform bookTransform1;
    Transform bookTransform2;
    Transform bookTransform3;

    //横の間隔
    public float widthSpace = 2.0f;

    public BookPrefabGenerator(GameObject bookPrefab, Transform bookTransform1, Transform bookTransform2, Transform bookTransform3)
    {
        this.bookPrefab = bookPrefab;
        this.bookTransform1 = bookTransform1;
        this.bookTransform2 = bookTransform2;
        this.bookTransform3 = bookTransform3;


        //全作品から10作品
        List<NovelDataLoadCSV.novelData> novels = NovelDataLoadCSV.novels;
        novelsGenerator(novels);


        //おすすめ作品から１０作品
    
        PopularGenerator(NovelDataLoadCSV.popularList);


        //過去履歴から１０作品

        resultGenerator(TopDire.historyList);
    }

    public void novelsGenerator(List<NovelDataLoadCSV.novelData> novels)
    {
        //リストの中をランダムに入れ替える
        novels = novels.OrderBy(x => Guid.NewGuid()).ToList();

        //10冊以上の場合１０にする
        int bookCount = Mathf.Min(novels.Count, 10);

        for (int i = 0; i < bookCount; i++)
        {
            //インスタンスを生成
            GameObject book = Instantiate(bookPrefab, GetPosition(i), Quaternion.identity, bookTransform1);

            //子オブジェクトを格納
            var bookC = GetChildren(book);

            //名前を設定
            book.name = "PickUp" + i;

            //作品名参照用　著者が増えたら変更する
            for (int j = 0; j < bookC.Length; j++)
            {
                if (bookC[j].name == "Title")
                {
                    bookC[j].GetComponent<TextMeshProUGUI>().text = novels[i].title;
                    
                    book.GetComponent<BookCiickedController>().title = novels[i].title;
                    book.GetComponent<BookCiickedController>().writer = novels[i].name;

                }else if (bookC[j].name == "Explanation")
                {
                    bookC[j].GetComponent<TextMeshProUGUI>().text = novels[i].name;
                }
            }

        }

    }

    public void PopularGenerator(List<NovelDataLoadCSV.novelData> popularList)
    {
        Debug.Log(popularList.Count);

        //リストの中をランダムに入れ替える
        popularList = popularList.OrderBy(x => Guid.NewGuid()).ToList();

        //10冊以上の場合１０にする
        int bookCount = Mathf.Min(popularList.Count, 10);

        for (int i = 0; i < bookCount; i++)
        {
            //インスタンスを生成
            GameObject book = Instantiate(bookPrefab, GetPosition(i), Quaternion.identity, bookTransform2);

            //子オブジェクトを格納
            var bookC = GetChildren(book);

            //名前を設定
            book.name = "おすすめ" + i;

            //作品名参照用　著者が増えたら変更する
            for (int j = 0; j < bookC.Length; j++)
            {
                if (bookC[j].name == "Title")
                {
                    bookC[j].GetComponent<TextMeshProUGUI>().text = popularList[i].title;
                    book.GetComponent<BookCiickedController>().title = popularList[i].title;
                    book.GetComponent<BookCiickedController>().writer = popularList[i].name;

                }
                else if (bookC[j].name == "Explanation")
                {
                    bookC[j].GetComponent<TextMeshProUGUI>().text = popularList[i].name;
                }
            }

        }

    }

    public void resultGenerator(List<NovelDataLoadCSV.novelData> result)
    {
        Debug.Log("動いた");
        if (result != null)
        {
            //10冊以上の場合１０にする
            int bookCount = result.Count;

            int n = 0;

            Debug.Log(bookCount);
         
                for (int i = bookCount-1; i>=0; i--)
                {
                    if (n == 10)
                    {
                        break;

                    }
                    //インスタンスを生成
                    GameObject book = Instantiate(bookPrefab, GetPosition(i), Quaternion.identity, bookTransform3);
                    Debug.Log("生成した");

                    Debug.Log(book.name);

                    //子オブジェクトを格納
                    var bookC = GetChildren(book);

                    //名前を設定
                    book.name = "検索履歴" + i;

                    //作品名参照用　著者が増えたら変更する
                    for (int j = 0; j < bookC.Length; j++)
                    {
                        if (bookC[j].name == "Title")
                        {
                            bookC[j].GetComponent<TextMeshProUGUI>().text = result[i].title;
                            book.GetComponent<BookCiickedController>().title = result[i].title;
                            book.GetComponent<BookCiickedController>().writer = result[i].name;
                            Debug.Log(result[i].name);
                        }
                        else if (bookC[j].name == "Explanation")
                        {
                            bookC[j].GetComponent<TextMeshProUGUI>().text = result[i].name;
                        }
                    }
                    n++;
                }  
        }

    }

    Vector3 GetPosition(int index)
    {
        float x = index * widthSpace * 0.3839f;
        float y = 0f;
        return new Vector3(x, y, 0f);
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
