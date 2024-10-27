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


        //�X���C�_�[�̍ő�l�̐ݒ�
        hpSlider.maxValue = maxHp;

        //�X���C�_�[�̌��ݒl�̐ݒ�
        hpSlider.value = nowHp;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Method()
    {
        Debug.Log("���ݒl�F" + hpSlider.value);

        if (hpSlider.value >= 50)
        {
            Debug.Log("50�ȏ�ł�");
        }

    }
}