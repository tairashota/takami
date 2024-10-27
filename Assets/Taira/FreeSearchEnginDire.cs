using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SearchEngineDire;

public class FreeSearchEnginDire : MonoBehaviour
{
    public TMP_InputField keyward;
    public static string keywards;
    public int searchDestination = 1;
    public static List<serachResult> serachResultDatas;
    public static List<string> serachResultDatas2;
    string keywardszenkai;
    public resultGenerator resultGenerator;
    public static int n = 0;

    [SerializeField] GameObject autoCorrectPrefab;
    [SerializeField] Transform panel;
    public AutoCorrectGen autoCorrectGen;
    public GameObject autoCorrectPanle;
    // Start is called before the first frame update
    void Start()
    { 
        if (n == 0)
        {
            serachResultDatas = SearchEngineDire.serachResultsList;
            n++;
        }
        resultGenerator.resultGenerators(serachResultDatas);
        keyward.onEndEdit.AddListener(OnEndEdit);
        panel.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        //キーワードの空白を削除の上、取得
        keywards = keyward.text.Trim();

        //キーワード欄が空白の場合は、回らないようにする。
        if (keyward != null)
        {
            //キーワード欄の内容が前回と同じ場合は、回らないようにする。
            if (keywardszenkai != keywards)
            {
                freeKeywardSearchEngine();
                autoCorrectGen.gens(serachResultDatas2, autoCorrectPrefab, panel);
            }
        }
    }

    //キーワード欄の内容に応じて、検索候補のリスト化
    public void freeKeywardSearchEngine()
    {
        if (keywards != "")
        {
            autoCorrectPanle.SetActive(true);
            serachResultDatas2 = new List<string>();
            foreach (var item in serachResultDatas)
            {

                if (item.bookTitle.Contains(keywards))
                {
                    serachResultDatas2.Add(item.bookTitle);
                }
                else if (item.bookWriters.Contains(keywards))
                {
                    serachResultDatas2.Add(item.bookWriters);
                }
            }
            keywardszenkai = keywards;
        }
        else
        {
            autoCorrectPanle.SetActive(false);
            serachResultDatas2 = new List<string>();
        }

    }

    public void OnEndEdit(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            keyward.text = ""; // 空文字の場合、テキストをクリア
        }
    }

    public void serachResultScene()
    {

        serachResultDatas = new List<serachResult>();
        foreach (var item in serachResultsList)
        {
            if (item.bookWriters.Contains(keywards))
            {
                serachResult serachResult = new serachResult();
                serachResult.bookWriters = item.bookWriters;
                serachResult.bookTitle = item.bookTitle;
                serachResultDatas.Add(serachResult);
            }
            if (item.bookTitle.Contains(keywards))
            {
                serachResult serachResult = new serachResult();
                serachResult.bookWriters = item.bookWriters;
                serachResult.bookTitle = item.bookTitle;
                serachResultDatas.Add(serachResult);
            }
        }

        SceneManager.LoadScene("AuthorScene");
    }
}
