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


    //�p�l���̃v���n�u������
    [SerializeField] GameObject panelPrefab;

    [SerializeField] Transform transformParent;
    //�p�l���̐�����(�y�[�W��)
    public static int panelCount;

    public GameObject cav;

    int pageCount = 0;

    int bookCount = 0;

    int panelPosition = 0;

    string[] paoj;

    //�{�^���̃v���n�u������
    [SerializeField] GameObject buttonPrefab;
    //�{�^���̐e�ɂȂ�ʒu������
    [SerializeField] Transform buttonTransform;

    GameObject lastPanel;

    public void resultGenerators(List<serachResult> serachResult)
    {
        pageCount = 0;
        bookCount = 0;
        panelPosition = 0;

        // ��������{���Ɋ�Â��ĕK�v�ȃp�l�������v�Z
        panelCount = (serachResult.Count / 9) + 1;
        // �K�v�Ȑ������p�l���𐶐�
        for (int i = 0; i < panelCount; i++)
        {
            // �p�l���𐶐����Đe�I�u�W�F�N�g�̎q�Ƃ��Ĕz�u
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


            //�y�[�W(�e)�̒��ɂ���{�i�q�j���擾
            var bookC = GetChildren(panelPage.gameObject);
            
            bookC[bookCount].GetComponent<BookCiickedController>().title = r.bookTitle;
            bookC[bookCount].GetComponent<BookCiickedController>().writer = r.bookWriters;
            Debug.Log(bookC[bookCount].GetComponent<BookCiickedController>().title);
            bookC[bookCount].SetActive(true);

            //�{(�e)�̒��ɂ�����(�q)���擾
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
        //�{(�e)�̒��ɂ�����(�q)���擾
        var lastbookCC = GetChildren(lastbookC[0]);
        Debug.Log(lastbookCC[0].GetComponent<TextMeshProUGUI>().text);
        Debug.Log(lastbookCC[0].activeSelf);
        if (lastbookCC[0].GetComponent<TextMeshProUGUI>().text == "���Җ�")
        {
            Debug.Log("���킵��");
            Destroy(lastPanel);
        }
    }

    public void buttonGenerator(int count)
    {
        for (int i = 0; i < count; i++)
        {
            //�{�^���̃C���X�^���X�𐶐�
            GameObject button = Instantiate(buttonPrefab, buttonTransform);
        }
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
