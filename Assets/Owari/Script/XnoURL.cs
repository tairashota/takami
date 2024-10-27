using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XnoURL : MonoBehaviour
{
    public void onClick()
    {
        Application.OpenURL("https://x.com/i/flow/login");//""の中には開きたいWebページのURLを入力します
    }

}

