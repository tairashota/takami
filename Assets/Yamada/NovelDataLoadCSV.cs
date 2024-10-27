using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NovelDataLoadCSV : MonoBehaviour
{
    //�e�L�X�g�t�@�C���̃p�X
    private string filePath = "/novelList.csv";

    //�������߃��X�g�̃t�@�C���p�X
    private string filePathPop = "/popularlList.csv";

    //��i�ƒ��҂̃��X�g�f�[�^���i�[����p
    public static List<novelData> novels = new List<novelData>();
    //��i�݂̂̃��X�g�f�[�^
    public static List<novelData> novelList = new List<novelData>();
    //���҂݂̂̃��X�g�f�[�^
    public static List<writerData> writers = new List<writerData>();
    //�������ߍ�i�̃��X�g�f�[�^
    public static List<novelData> popularList = new List<novelData>();


    bool pop = false;

    //��i�ƒ��҂̃N���X
    public class novelData
    {
        //��i�� + ����
        public string title;
        //��i�� + ����\�[�g(����)
        public string titleSort;
        //����
        public string titleSub;
        //���Ґ���
        public string name;
        //���Ґ����\�[�g
        public string nameSort;

        public novelData(string title)
        {
            //��i�{��i�\�[�g�p
            this.title = title;
        }

        public novelData(string title , string name)
        {
            this.title = title;
            this.name = name;
        }

        public novelData(string title, string titleSort ,string titleSub, string name, string nameSort)
        { 
            //��i�� 
            this.title = title;
            //��i�� �\�[�g(����)
            this.titleSort = titleSort;
            //����
            this.titleSub = titleSub;

            //���Ґ���
            this.name = name;
            //���Ґ����\�[�g
            this.nameSort = nameSort;
        }
    }


    public class writerData
    {
        //���Ґ���
        public string name;
        
        public writerData(string Name)
        {
            //���Ґ���
            name = Name;
        }
    }



    public List<novelData> LoadCSV(string fileName, List<novelData>list) //novelList.csv���烊�X�g���쐬
    {
        // �ǂݍ��݂���CSV�t�@�C���̃p�X���w�肵�ĊJ��
        StreamReader sr = new StreamReader(Application.dataPath + fileName);
        {
            
                // �����܂ŌJ��Ԃ�
                while (!sr.EndOfStream)
                {
                    // CSV�t�@�C���̈�s��ǂݍ���
                    string line = sr.ReadLine();
                    // �ǂݍ��񂾈�s���J���}���ɕ����Ĕz��Ɋi�[����
                    //value[0]��i��, [1]�\�[�g�p�ǂ�, [2]����, [3]��, [4]��, [5]���ǂ݃\�[�g�p, [6]���ǂ݃\�[�g�p, [7]��i���쌠�t���O
                    string[] values = line.Split(',');

                    //���쌠���Ȃ��̂��݂̂̂��E��
                    if (values[7] == "�Ȃ�")
                    {
                        novelData noveldata = new novelData(values[0], values[1], values[2], values[3] + values[4], values[5] + values[6]);
                    list.Add(noveldata);
                    }
                }
            
        }
        return list;
    }



    public List<novelData> NovelListLoad(List<novelData> novels)//��i���X�g�̍쐬
    {
        
        List<novelData>novelsData  = new List<novelData>();

        for (int i = 0; i < novels.Count; i++)
        {
            //��i���ƍ�i�\�[�g�����݂ɑ}��
            novelData nameData1 = new novelData(novels[i].title);
            novelData nameData2 = new novelData(novels[i].titleSort);

            novelsData.Add(nameData1);
            novelsData.Add(nameData2);

        }

        //�d�����Ȃ��悤�ɂ���
        novelList = novelsData.GroupBy(n => new { n.title }).Select(g => g.First()).ToList();

        return novelList;
    }

    public List<writerData> WriterLoad(List<novelData> novels)//���҃��X�g�̍쐬
    {

        List<writerData> writersData = new List<writerData>();

        for (int i = 0; i < novels.Count; i++)
        {
            //���Җ��ƒ��Җ��\�[�g�����݂ɑ}��
            writerData nameData1 = new writerData(novels[i].name);
            writerData nameData2 = new writerData(novels[i].nameSort);

            writersData.Add(nameData1);
            writersData.Add(nameData2);

        }

        //�d�����Ȃ��悤�ɂ���
        writers = writersData.GroupBy(n => new {n.name}).Select(g => g.First()).ToList(); 

        return writers;
    }





    public NovelDataLoadCSV()
    {
        novelList.Clear();
        writers.Clear();
        novels.Clear();
        popularList.Clear();

        //CSV�����i�A���҂̃��X�g�f�[�^���쐬
        LoadCSV(filePath, novels);

        //�������ߍ�i�̃��X�g�f�[�^�̍쐬
        LoadCSV(filePathPop, popularList);

        //��i�݂̂̃��X�g�f�[�^�̍쐬 
        novelList = NovelListLoad(novels);
        //���҂݂̂̃��X�g�f�[�^�̍쐬
        writers = WriterLoad(novels);
       



        /*�f�o�b�O�p

        foreach (novelData novel in novels)
        {
           //��i�A���҂��ׂĂ��܂߂����X�g
            Debug.Log($"��i�� : {novel.title},��i���\�[�g : {novel.titleSort},���� :{novel.titleSub},���Ґ��� : {novel.name},���Ґ����\�[�g : {novel.nameSort}");
        } */
        /*
        foreach (novelData novel in novelList)
        {
            //��i�����̃��X�g
            Debug.Log($"��i�� : {novel.title} ");
        }
        */
        /*
       foreach (writerData writerData in writers)
       {
           //���҂����̃��X�g
           Debug.Log($"���Ґ� : {writerData.name}");
       }
        
        foreach (novelData novel in popularList)
        {
           
            Debug.Log($"��i�� : {novel.title},��i���\�[�g : {novel.titleSort},���� :{novel.titleSub},���Ґ��� : {novel.name},���Ґ����\�[�g : {novel.nameSort}");
        }
        */
       

    }
}

