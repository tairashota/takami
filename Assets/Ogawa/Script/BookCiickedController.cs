using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookCiickedController : MonoBehaviour
{
    public string title;
    public string writer;

    public string iwakonoYouScene;

    public void SwitchSceneTop()
    {
        TopDire.titleName = title;
        TopDire.writerName = writer;
        Debug.Log(TopDire.titleName + TopDire.writerName);
        TopDire.flagTop = true;
        SceneManager.LoadScene("iwakinoYouScene");
    }

    public void SwitchSceneResult()
    {
        TopDire.titleName = title;
        TopDire.writerName = writer;
        Debug.Log(TopDire.titleName + TopDire.writerName);
        SceneManager.LoadScene("iwakinoYouScene");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
