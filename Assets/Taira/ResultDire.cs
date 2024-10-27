using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using TMPro;
using UnityEngine;
using static SearchEngineDire;

public class ResultDire : MonoBehaviour
{
    public resultGenerator resultGenerator;
    [SerializeField] Transform panel;
    public TextMeshProUGUI author,hit;

    void Start()
    {
        author.text = "キーワード「"+SearchEngineDire.resultW + "」";
        hit.text = "ヒット" + serachResultsList.Count + "件";
    }

    // Update is called once per frame
    public void Gen(List<serachResult> serachResult)
    {
        resultGenerator.resultGenerators(serachResult);
    }
}
