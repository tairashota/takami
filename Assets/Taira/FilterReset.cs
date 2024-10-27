using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FilterReset : MonoBehaviour
{

    public void resetClick()
    {
        FreeSearchEnginDire.serachResultDatas = SearchEngineDire.serachResultsList;
        SceneManager.LoadScene("AuthorScene");
    }

}
