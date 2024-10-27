using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static SearchEngineDire;
using static NovelDataLoadCSV;
public class TopDire : MonoBehaviour
{
    public static string writerName, titleName;
 
    NovelDataLoadCSV NovelDataLoadCSV;
    public static List<novelData> historyList = new List<novelData>();

    BookPrefabGenerator bookPrefabGenerator;

    [SerializeField] GameObject bookPrefab;

    [SerializeField] Transform bookTransform1;
    [SerializeField] Transform bookTransform2;
    [SerializeField] Transform bookTransform3;

    public static bool flagTop = false;


    // Start is called before the first frame update
    void Start()
    {
        flagTop = false;

        NovelDataLoadCSV = new NovelDataLoadCSV();

        if (writerName != null)
        {
            bool b = true;
            foreach(var item in historyList)
            {
                if(item.title == titleName && item.name == writerName)
                {
                    b = false;

                }        
            }
            if (b)
            {
                historyList.Add(new novelData(titleName, writerName));
            }
        }
        bookPrefabGenerator = new BookPrefabGenerator(bookPrefab, bookTransform1, bookTransform2, bookTransform3);
        SearchEngineDire.serachResultsList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
