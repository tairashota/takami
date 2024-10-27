using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Image Musimegane2; // �؂�ւ���Image�I�u�W�F�N�g
    public Sprite[] images; // �؂�ւ������摜�̔z��
    private int currentIndex = 0; // ���݂̉摜�̃C���f�b�N�X

    void Start()
    {
        // �ŏ��̉摜��ݒ�
        if (images.Length > 0)
        {
            Musimegane2.sprite = images[currentIndex];
        }
    }

    public void SwitchImage()
    {
        // �C���f�b�N�X���X�V
        currentIndex = (currentIndex + 1) % images.Length; // ���̉摜�Ɉړ��i���[�v�j

        // �摜��؂�ւ���
        Musimegane2.sprite = images[currentIndex];
    }
}
    

