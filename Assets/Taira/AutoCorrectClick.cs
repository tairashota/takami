using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoCorrectClick : MonoBehaviour
{
    public void serachResultScene()
    {
        var autoCorrectC = GetChildren(this.gameObject);
        //����������łȂ����
        if (SearchEngineDire.keywards != "")
        {
            //���Җ���CSV����ǂݍ��񂾃��X�g�ɑ΂��Č���
            if (SearchEngineDire.searchDestination == 0)
            {
                foreach (var item in NovelDataLoadCSV.novels)
                {
                    if (item.name.StartsWith(autoCorrectC[0].GetComponent<TextMeshProUGUI>().text) || item.nameSort.StartsWith(autoCorrectC[0].GetComponent<TextMeshProUGUI>().text))
                    {
                        SearchEngineDire.serachResult serachResult = new SearchEngineDire.serachResult();
                        serachResult.bookWriters = item.name;
                        serachResult.bookTitle = item.title;

                        //�������ʃ��X�g�֒ǉ�
                        SearchEngineDire.serachResultsList.Add(serachResult);
                    }
                }
            }
            //��i����CSV����ǂݍ��񂾃��X�g�ɑ΂��Č���
            else if (SearchEngineDire.searchDestination == 1)
            {
                foreach (var item in NovelDataLoadCSV.novels)
                {
                    if (item.title.StartsWith(autoCorrectC[0].GetComponent<TextMeshProUGUI>().text) || item.titleSort.StartsWith(autoCorrectC[0].GetComponent<TextMeshProUGUI>().text))
                    {

                        SearchEngineDire.serachResult serachResult = new SearchEngineDire.serachResult();
                        serachResult.bookWriters = item.name;
                        serachResult.bookTitle = item.title;

                        //�������ʃ��X�g�֒ǉ�
                        SearchEngineDire.serachResultsList.Add(serachResult);
                    }
                }
            }
            SearchEngineDire.resultW = autoCorrectC[0].GetComponent<TextMeshProUGUI>().text;
            FreeSearchEnginDire.n = 0;
            SceneManager.LoadScene("AuthorScene");
        }
        
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
}
