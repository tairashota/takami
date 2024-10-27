using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene2 : MonoBehaviour
{
    public void OnClick()
    {
        // 遷移させたいシーンの名前が「Next」の場合
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

