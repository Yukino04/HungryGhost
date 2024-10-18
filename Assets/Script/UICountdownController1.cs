using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountdownController1 : MonoBehaviour
{
    // �J�E���g�_�E����\�����邽�߂�Text UI�R���|�[�l���g��ݒ肵�܂�
    public Text countdownText;

    // GameController�̃C���X�^���X��ێ�����ϐ�
    GameController gameController;

    void Start()
    {
        // GameController�R���|�[�l���g���擾���܂�
        gameController = FindObjectOfType<GameController>();

        // �����\������ɂ��܂�
        countdownText.text = "";
    }

    void Update()
    {
        // �Q�[�����I�����Ă��Ȃ��ꍇ�̂݃J�E���g�_�E�����X�V���܂�
        if (!gameController.IsGameEnded())
        {
            // �c�莞�Ԃ��擾���܂�
            float remainingTime = gameController.GetRemainingTime();

            // �c��5�b�ȉ��ɂȂ�����J�E���g�_�E����\�����܂�
            if (remainingTime <= 5f && remainingTime > 0f)
            {
                countdownText.text = Mathf.Ceil(remainingTime).ToString();
            }
            else
            {
                // 5�b�ȏ�̏ꍇ�̓e�L�X�g���\���ɂ��܂�
                countdownText.text = "";
            }
        }
        else
        {
            // �Q�[���I�����̓e�L�X�g���\���ɂ��܂�
            countdownText.text = "";
        }
    }
}