using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI BookName;
    public void onButtonClick()
    {
        ChatGPTExample.name = Name.text;
        ChatGPTExample.bookName = BookName.text;
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
