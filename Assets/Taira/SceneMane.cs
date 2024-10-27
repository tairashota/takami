using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMane : MonoBehaviour
{
    public void topClick()
    {
        SceneManager.LoadScene("TopScene");
    }

    public void resultClick()
    {
        SceneManager.LoadScene("AuthorScene");
    }
    
}
