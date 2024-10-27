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
    //�{�̃v���n�u������
    GameObject bookPrefab;

    //�p�l���̐e������
    Transform bookTransform1;
    Transform bookTransform2;
    Transform bookTransform3;

    //���̊Ԋu
    public float widthSpace = 2.0f;

    public BookPrefabGenerator(GameObject bookPrefab, Transform bookTransform1, Transform bookTransform2, Transform bookTransform3)
    {
        this.bookPrefab = bookPrefab;
        this.bookTransform1 = bookTransform1;
        this.bookTransform2 = bookTransform2;
        this.bookTransform3 = bookTransform3;


        //�S��i����10��i
        List<NovelDataLoadCSV.novelData> novels = NovelDataLoadCSV.novels;
        novelsGenerator(novels);


        //�������ߍ�i����P�O��i
    
        PopularGenerator(NovelDataLoadCSV.popularList);


        //�ߋ���������P�O��i

        resultGenerator(TopDire.historyList);
    }

    public void novelsGenerator(List<NovelDataLoadCSV.novelData> novels)
    {
        //���X�g�̒��������_���ɓ���ւ���
        novels = novels.OrderBy(x => Guid.NewGuid()).ToList();

        //10���ȏ�̏ꍇ�P�O�ɂ���
        int bookCount = Mathf.Min(novels.Count, 10);

        for (int i = 0; i < bookCount; i++)
        {
            //�C���X�^���X�𐶐�
            GameObject book = Instantiate(bookPrefab, GetPosition(i), Quaternion.identity, bookTransform1);

            //�q�I�u�W�F�N�g���i�[
            var bookC = GetChildren(book);

            //���O��ݒ�
            book.name = "PickUp" + i;

            //��i���Q�Ɨp�@���҂���������ύX����
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

        //���X�g�̒��������_���ɓ���ւ���
        popularList = popularList.OrderBy(x => Guid.NewGuid()).ToList();

        //10���ȏ�̏ꍇ�P�O�ɂ���
        int bookCount = Mathf.Min(popularList.Count, 10);

        for (int i = 0; i < bookCount; i++)
        {
            //�C���X�^���X�𐶐�
            GameObject book = Instantiate(bookPrefab, GetPosition(i), Quaternion.identity, bookTransform2);

            //�q�I�u�W�F�N�g���i�[
            var bookC = GetChildren(book);

            //���O��ݒ�
            book.name = "��������" + i;

            //��i���Q�Ɨp�@���҂���������ύX����
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
        Debug.Log("������");
        if (result != null)
        {
            //10���ȏ�̏ꍇ�P�O�ɂ���
            int bookCount = result.Count;

            int n = 0;

            Debug.Log(bookCount);
         
                for (int i = bookCount-1; i>=0; i--)
                {
                    if (n == 10)
                    {
                        break;

                    }
                    //�C���X�^���X�𐶐�
                    GameObject book = Instantiate(bookPrefab, GetPosition(i), Quaternion.identity, bookTransform3);
                    Debug.Log("��������");

                    Debug.Log(book.name);

                    //�q�I�u�W�F�N�g���i�[
                    var bookC = GetChildren(book);

                    //���O��ݒ�
                    book.name = "��������" + i;

                    //��i���Q�Ɨp�@���҂���������ύX����
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


    // parent�����̎q�I�u�W�F�N�g��for���[�v�Ŏ擾����
    private static GameObject[] GetChildren(GameObject parent)
    {
        // �e�I�u�W�F�N�g��Transform���擾
        var parentTransform = parent.transform;

        // �q�I�u�W�F�N�g���i�[����z��쐬
        var children = new GameObject[parentTransform.childCount];

        // 0�`��-1�܂ł̎q�����Ԃɔz��Ɋi�[
        for (var i = 0; i < children.Length; ++i)
        {
            // Transform����Q�[���I�u�W�F�N�g���擾���Ċi�[
            children[i] = parentTransform.GetChild(i).gameObject;
        }

        // �q�I�u�W�F�N�g���i�[���ꂽ�z��
        return children;
    }

}
