using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeAutoCorrectClick : MonoBehaviour
{

    GameObject Topdire;
    public resultGenerator resultGenerator;

    private void Start()
    {
        Topdire = GameObject.Find("ResultGenerator");
    }
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

    public void serachResultScene()
    {

            FreeSearchEnginDire.serachResultDatas = new List<SearchEngineDire.serachResult>();
        foreach (var item in SearchEngineDire.serachResultsList)
        {
            if (item.bookWriters.Contains(FreeSearchEnginDire.keywards))
            {
                SearchEngineDire.serachResult serachResult = new SearchEngineDire.serachResult();
                serachResult.bookWriters = item.bookWriters;
                serachResult.bookTitle = item.bookTitle;
                FreeSearchEnginDire.serachResultDatas.Add(serachResult);
            }
            if (item.bookTitle.Contains(FreeSearchEnginDire.keywards))
            {
                SearchEngineDire.serachResult serachResult = new SearchEngineDire.serachResult();
                serachResult.bookWriters = item.bookWriters;
                serachResult.bookTitle = item.bookTitle;
                FreeSearchEnginDire.serachResultDatas.Add(serachResult);
            }
            Debug.Log(item.bookTitle);
        }

        SceneManager.LoadScene("AuthorScene");
    }
}
