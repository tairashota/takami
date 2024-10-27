using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Slider hpSlider;

    // Use this for initialization
    void Start()
    {

        hpSlider = GetComponent<Slider>();

        float maxHp = 200f;
        float nowHp = 40f;


        //スライダーの最大値の設定
        hpSlider.maxValue = maxHp;

        //スライダーの現在値の設定
        hpSlider.value = nowHp;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Method()
    {
        Debug.Log("現在値：" + hpSlider.value);

        if (hpSlider.value >= 50)
        {
            Debug.Log("50以上です");
        }

    }
}