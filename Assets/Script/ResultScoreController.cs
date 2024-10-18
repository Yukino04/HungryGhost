using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText; // ���U���g��ʂɕ\������X�R�A�̃e�L�X�gUI

    void Start()
    {
        // ScoreManager����X�R�A���擾���ĕ\��
        scoreText.text = "���傤�̃��V���N��" + ResultScoreManager.currentScore.ToString() + "�R����";
    }
    void Update()
    {
        //ESCkey�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //�Q�[�����I��
            Application.Quit();
        }
    }
}