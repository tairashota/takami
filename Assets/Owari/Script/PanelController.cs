using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;



public class PanelController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] float extRate = 1.1f;
    [SerializeField] float time = 0.2f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(extRate, extRate, 1), "time", time, "easeType", iTween.EaseType.easeOutBack));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", time, "easeType", iTween.EaseType.easeOutBack));
    }


    public void OnClick()
    {
        // �J�ڂ��������V�[���̖��O���uNext�v�̏ꍇ
        SceneManager.LoadScene("iwakinoYouScene");
    }
}



