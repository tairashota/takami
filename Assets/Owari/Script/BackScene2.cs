using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene2 : MonoBehaviour
{
    public void OnClick()
    {
        // �J�ڂ��������V�[���̖��O���uNext�v�̏ꍇ
        if (TopDire.flagTop)
        {
            SceneManager.LoadScene("TopScene");
        }
        else
        {
            SceneManager.LoadScene("AuthorScene");
        }
    }
}

