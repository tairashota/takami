using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NovelDataLoadCSV : MonoBehaviour
{
    //テキストファイルのパス
    private string filePath = "/novelList.csv";

    //おすすめリストのファイルパス
    private string filePathPop = "/popularlList.csv";

    //作品と著者のリストデータを格納する用
    public static List<novelData> novels = new List<novelData>();
    //作品のみのリストデータ
    public static List<novelData> novelList = new List<novelData>();
    //著者のみのリストデータ
    public static List<writerData> writers = new List<writerData>();
    //おすすめ作品のリストデータ
    public static List<novelData> popularList = new List<novelData>();


    bool pop = false;

    //作品と著者のクラス
    public class novelData
    {
        //作品名 + 副題
        public string title;
        //作品名 + 副題ソート(かな)
        public string titleSort;
        //副題
        public string titleSub;
        //著者姓名
        public string name;
        //著者姓名ソート
        public string nameSort;

        public novelData(string title)
        {
            //作品＋作品ソート用
            this.title = title;
        }

        public novelData(string title , string name)
        {
            this.title = title;
            this.name = name;
        }

        public novelData(string title, string titleSort ,string titleSub, string name, string nameSort)
        { 
            //作品名 
            this.title = title;
            //作品名 ソート(かな)
            this.titleSort = titleSort;
            //副題
            this.titleSub = titleSub;

            //著者姓名
            this.name = name;
            //著者姓名ソート
            this.nameSort = nameSort;
        }
    }


    public class writerData
    {
        //著者姓名
        public string name;
        
        public writerData(string Name)
        {
            //著者姓名
            name = Name;
        }
    }



    public List<novelData> LoadCSV(string fileName, List<novelData>list) //novelList.csvからリストを作成
    {
        // 読み込みたいCSVファイルのパスを指定して開く
        StreamReader sr = new StreamReader(Application.dataPath + fileName);
        {
            
                // 末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string line = sr.ReadLine();
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    //value[0]作品名, [1]ソート用読み, [2]副題, [3]姓, [4]名, [5]姓読みソート用, [6]名読みソート用, [7]作品著作権フラグ
                    string[] values = line.Split(',');

                    //著作権がなしのもののみを拾う
                    if (values[7] == "なし")
                    {
                        novelData noveldata = new novelData(values[0], values[1], values[2], values[3] + values[4], values[5] + values[6]);
                    list.Add(noveldata);
                    }
                }
            
        }
        return list;
    }



    public List<novelData> NovelListLoad(List<novelData> novels)//作品リストの作成
    {
        
        List<novelData>novelsData  = new List<novelData>();

        for (int i = 0; i < novels.Count; i++)
        {
            //作品名と作品ソートを交互に挿入
            novelData nameData1 = new novelData(novels[i].title);
            novelData nameData2 = new novelData(novels[i].titleSort);

            novelsData.Add(nameData1);
            novelsData.Add(nameData2);

        }

        //重複しないようにする
        novelList = novelsData.GroupBy(n => new { n.title }).Select(g => g.First()).ToList();

        return novelList;
    }

    public List<writerData> WriterLoad(List<novelData> novels)//著者リストの作成
    {

        List<writerData> writersData = new List<writerData>();

        for (int i = 0; i < novels.Count; i++)
        {
            //著者名と著者名ソートを交互に挿入
            writerData nameData1 = new writerData(novels[i].name);
            writerData nameData2 = new writerData(novels[i].nameSort);

            writersData.Add(nameData1);
            writersData.Add(nameData2);

        }

        //重複しないようにする
        writers = writersData.GroupBy(n => new {n.name}).Select(g => g.First()).ToList(); 

        return writers;
    }





    public NovelDataLoadCSV()
    {
        novelList.Clear();
        writers.Clear();
        novels.Clear();
        popularList.Clear();

        //CSVから作品、著者のリストデータを作成
        LoadCSV(filePath, novels);

        //おすすめ作品のリストデータの作成
        LoadCSV(filePathPop, popularList);

        //作品のみのリストデータの作成 
        novelList = NovelListLoad(novels);
        //著者のみのリストデータの作成
        writers = WriterLoad(novels);
       



        /*デバッグ用

        foreach (novelData novel in novels)
        {
           //作品、著者すべてを含めたリスト
            Debug.Log($"作品名 : {novel.title},作品名ソート : {novel.titleSort},副題 :{novel.titleSub},著者姓名 : {novel.name},著者姓名ソート : {novel.nameSort}");
        } */
        /*
        foreach (novelData novel in novelList)
        {
            //作品だけのリスト
            Debug.Log($"作品名 : {novel.title} ");
        }
        */
        /*
       foreach (writerData writerData in writers)
       {
           //著者だけのリスト
           Debug.Log($"著者姓 : {writerData.name}");
       }
        
        foreach (novelData novel in popularList)
        {
           
            Debug.Log($"作品名 : {novel.title},作品名ソート : {novel.titleSort},副題 :{novel.titleSub},著者姓名 : {novel.name},著者姓名ソート : {novel.nameSort}");
        }
        */
       

    }
}

