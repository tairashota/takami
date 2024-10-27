using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using System.Xml.Linq;
using static NovelDataLoadCSV;
using UnityEngine.UI;
using System.Drawing;
using UnityEngine.SceneManagement;

public class SearchEngineDire : MonoBehaviour
{
    public static List<string> bookTitleList = new List<string>();
    public static List<string> recomendationList = new List<string>();
    public TMP_InputField keyward;
    public Toggle title, writers;

    public static string keywards;
    public static string resultW;
    public static int searchDestination = 1;
    public static List<string> serachResultDatas;
    public static List<serachResult> serachResultsList;
    string keywardszenkai;
    [SerializeField] GameObject autoCorrectPrefab;
    [SerializeField] Transform panel;
    public AutoCorrectGen autoCorrectGen;
    public GameObject autoCorrectPanle;
    // Start is called before the first frame update
    void Start()
    {

        serachResultDatas = new List<string>();
        serachResultsList = new List<serachResult>();
        keyward.onEndEdit.AddListener(OnEndEdit);
        searchDestination = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (title.isOn)
        {
            searchDestination = 1;
        }
        if (writers.isOn) 
        {
            searchDestination = 0;
        }
        keywards = keyward.text.Trim();
        if(keyward != null)
        {
            if (keywardszenkai != keywards)
            {
                keywardSearchEngune();
                if (searchDestination == 1)
                {
                    autoCorrectGen.gen(serachResultDatas, autoCorrectPrefab, panel);
                }
                if (searchDestination == 0)
                {
                    autoCorrectGen.gen(serachResultDatas, autoCorrectPrefab, panel);
                }
            }
        }
        
    }

    public class serachResult
    {
        public string bookTitle, bookWriters;
    }

    public void keywardSearchEngune()
    {
        if(keywards != "")
        {
            autoCorrectPanle.SetActive(true);
            serachResultDatas = new List<string>();
            if (searchDestination == 0)
            {
                Debug.Log(keywards.Length);
                foreach (var item in NovelDataLoadCSV.writers)
                {

                    if (item.name.StartsWith(keywards))
                    {
                        serachResultDatas.Add(item.name);
                        Debug.Log(item.name);
                    }
                }
                keywardszenkai = keywards;
            }
            else if (searchDestination == 1)
            {
                Debug.Log(keywards);
                foreach (var item in NovelDataLoadCSV.novelList)
                {
                    if (item.title.StartsWith(keywards))
                    {
                        serachResultDatas.Add(item.title);
                        Debug.Log(item.title);
                    }
                }
                keywardszenkai = keywards;
            }

        }
        else
        {
            autoCorrectPanle.SetActive(false);
            serachResultDatas = new List<string>();
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
        if (keywards != "")
        {
            if (searchDestination == 0)
            {
                foreach (var item in NovelDataLoadCSV.novels)
                {
                    if (item.name.StartsWith(keywards)|| item.nameSort.StartsWith(keywards))
                    {
                        serachResult serachResult = new serachResult();
                        serachResult.bookWriters = item.name;
                        serachResult.bookTitle = item.title;
                        serachResultsList.Add(serachResult);
                    }
                }
            }
            else if (searchDestination == 1)
            {
                foreach (var item in NovelDataLoadCSV.novels)
                {
                    if (item.title.StartsWith(keywards) || item.titleSort.StartsWith(keywards))
                    {
                        serachResult serachResult = new serachResult();
                        serachResult.bookWriters = item.name;
                        serachResult.bookTitle = item.title;
                        serachResultsList.Add(serachResult);
                    }
                }
            }

            resultW = keywards;
            //検索結果シーン推移
            FreeSearchEnginDire.n = 0;
            SceneManager.LoadScene("AuthorScene");
        }
    }
}