using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImageController : MonoBehaviour
{
    public Image targetImage; // ������Image�R���|�[�l���g���A�T�C��
    public Sprite[] images;   // �摜���i�[����z��

    // private ImageConversion image;

    // Start is called before the first frame update
    void Start()
    {
        if (images.Length > 0 && targetImage != null)
        {
            // �����_���ȃC���f�b�N�X�𐶐�
            int randomIndex = Random.Range(0, images.Length);
            // �����_���ɑI�񂾉摜��ݒ�
            targetImage.sprite = images[randomIndex];
        }
        else
        {
            Debug.LogError("Images array is empty or targetImage is not assigned.");
        }

    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
