using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static SearchEngineDire;

public class AutoCorrectGen : MonoBehaviour
{
    int i = 0;
    public void gen(List<string> bookList, GameObject autoCorrectPrefab, Transform panel)
    {
        bookList = bookList.OrderBy(x => Guid.NewGuid()).ToList();
        foreach (Transform n in panel)
        {
            GameObject.Destroy(n.gameObject);
        }
        int bookCount = Mathf.Min(bookList.Count, 10);
        for (int i = 0; i < bookCount; i++)
        {
            GameObject autoCorrect = Instantiate(autoCorrectPrefab, new Vector3(175.5f, 667.2f - 50 * i * 0.3839f, 0.0f), Quaternion.identity, panel);
            var autoCorrectC = GetChildren(autoCorrect);
            autoCorrectC[0].GetComponent<TextMeshProUGUI>().text = bookList[i];
        }
    }
    public void gens(List<string> bookList, GameObject autoCorrectPrefab, Transform panel)
    {
        bookList = bookList.OrderBy(x => Guid.NewGuid()).ToList();
        foreach (Transform n in panel)
        {
            GameObject.Destroy(n.gameObject);
        }
        int bookCount = Mathf.Min(bookList.Count, 10);
        for (int i = 0; i < bookCount; i++)
        {
            GameObject autoCorrect = Instantiate(autoCorrectPrefab, new Vector3(500.0f * 0.3439f, 1725.0f * 0.3839f - 50 * i * 0.3839f, 0.0f), Quaternion.identity, panel);
            var autoCorrectC = GetChildren(autoCorrect);
            autoCorrectC[0].GetComponent<TextMeshProUGUI>().text = bookList[i];
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
