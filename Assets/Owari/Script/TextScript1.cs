using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScript1 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NoveNutshellt;

    void Start()
    {
        NoveNutshellt.text = "";
        NoveNutshellt.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        NoveNutshellt.text = "Novel Nutshell";
        while (true)
        {
            for (int i = 0; i < 255; i++)
            {
                NoveNutshellt.color = NoveNutshellt.color + new Color32(1, 1, 1, 1);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}