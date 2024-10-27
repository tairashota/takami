using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour
{
    public void OnClick()
    {
        // 遷移させたいシーンの名前が「Next」の場合
        SceneManager.LoadScene("TopScene");
    }
}
    