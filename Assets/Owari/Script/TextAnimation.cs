using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;

    [SerializeField, Tooltip("1�b�Ԃɂ�����s�����x�̑�����")]
    private float FadeSpeed = 2.0f;

    private Coroutine animationCoroutine;

    public void Restart()
    {
        Run();
    }

    private void Start()
    {
        Run();
    }

    private void Run()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(Seamless());
    }

    private IEnumerator Seamless()
    {
        // script��Ńe�L�X�g���X�V�����ꍇ�ATMP�̍X�V���I����Ă��Ȃ��ꍇ������̂ōĐ���
        tmpText.ForceMeshUpdate(true);
        TMP_TextInfo textInfo = tmpText.textInfo;
        TMP_CharacterInfo[] charInfos = textInfo.characterInfo;

        // �S�Ă̕�������x��\���ɂ���(���ꕶ���̌��ˍ����ŗv�f�ƕ����̐�����v���Ȃ��ꍇ������)
        for (var i = 0; i < charInfos.Length; i++)
        {
            SetTextAlpha(tmpText, i, 0);
        }

        // charInfos�̗v�f�������[�v
        for (var i = 0; i < charInfos.Length; i++)
        {
            // �󔒂܂��͉��s�����̏ꍇ�͖���
            if (char.IsWhiteSpace(charInfos[i].character)) continue;

            // �ꕶ�����Ƃ�0.2�b�ҋ@
            yield return new WaitForSeconds(0.2f);

            float alpha = 0.0f;

            while (true)
            {
                // FixedUpdate�̃^�C�~���O�܂ő҂�
                yield return new WaitForFixedUpdate();

                // �ꕶ���̕s�����x�𑝉������Ă���
                // �b�P�ʂ���t���[���P�ʂɕϊ�
                float alphaDelta = FadeSpeed * Time.fixedDeltaTime;
                alpha = Mathf.Min(alpha + alphaDelta, 1.0f);
                SetTextAlpha(tmpText, i, (byte)(255 * alpha));

                // �s�����x��1.0�𒴂����玟�̕����Ɉڂ�
                if (alpha >= 1.0f) break;
            }

        }
    }

    // charIndex�Ŏw�肵�������̓����x��ύX
    private void SetTextAlpha(TMP_Text text, int charIndex, byte alpha)
    {
        // charIndex�Ԗڂ̕����̃f�[�^�\���̂��擾
        TMP_TextInfo textInfo = text.textInfo;
        TMP_CharacterInfo charInfo = textInfo.characterInfo[charIndex];

        // �������\�����郁�b�V��(��`)���擾
        TMP_MeshInfo meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

        // ��`�Ȃ̂�4���_
        var rectVerticesNum = 4;
        for (var i = 0; i < rectVerticesNum; ++i)
        {
            // �ꕶ�����\�������`�̒��_�̓����x��ύX
            meshInfo.colors32[charInfo.vertexIndex + i].a = alpha;
        }

        // ���_�J���[��ύX�������Ƃ�ʒm
        text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

}
