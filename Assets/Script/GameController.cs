using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timeUpText; // "Time Up"��\�����邽�߂�UI
    public Text gameOverText; // "Game Over"��\�����邽�߂�UI
    public float gameDuration; // �Q�[���̑����ԁi�b�j
    float remainingTime; // �c�莞��
    bool isGameEnded = false; // �Q�[�����I���������ǂ����̃t���O

    void Start()
    {
        timeUpText.gameObject.SetActive(false); // �ŏ���"Time Up"���\���ɂ���
        gameOverText.gameObject.SetActive(false); // �ŏ���"Game Over"���\���ɂ���
        remainingTime = gameDuration; // �c�莞�Ԃ��Q�[���̑����Ԃɐݒ�
    }

    void Update()
    {
        //ESCkey�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //�Q�[�����I��
            Application.Quit();
        }
        if (!isGameEnded)
        {
            remainingTime -= Time.deltaTime; // �c�莞�Ԃ����炷
            if (remainingTime <= 0)
            {
                EndGame(); // �c�莞�Ԃ�0�ȉ��ɂȂ�����Q�[���I���������Ă�
            }
        }
        else
        {
            // �Q�[���I����A�X�y�[�X�L�[�������ꂽ�烊�U���g��ʂɑJ��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeToResultScene();
            }
        }
    }

    public void EndGame()
    {
        isGameEnded = true;
        remainingTime = 0; // �c�莞�Ԃ�0�ȉ��ɂ����Ȃ��悤�ɐݒ�
        timeUpText.gameObject.SetActive(true); // "Time Up"��\������
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.DisableControl(); // �v���C���[�̑���𖳌���
        }
        // �X�R�A��ÓI�ϐ��ɕۑ�
        ResultScoreManager.currentScore = FindObjectOfType<UIPlayerScoreController>().GetScore();
    }

    public void GameOver()
    {
        isGameEnded = true;
        gameOverText.gameObject.SetActive(true); // "Game Over"��\������
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.DisableControl(); // �v���C���[�̑���𖳌���
        }
        GameFruitManager gameFruitManager = FindObjectOfType<GameFruitManager>();
        if (gameFruitManager != null)
        {
            gameFruitManager.enabled = false; // �A�C�e���������~
        }
        BoxManager boxManager = FindObjectOfType<BoxManager>();
        if (boxManager != null)
        {
            boxManager.enabled = false; // �{�b�N�X�������~
        }
        // �X�R�A��ÓI�ϐ��ɕۑ�
        ResultScoreManager.currentScore = FindObjectOfType<UIPlayerScoreController>().GetScore();
    }

    private void ChangeToResultScene()
    {
        SceneManager.LoadScene("ResultScene"); // �w�肳�ꂽ���U���g�V�[���ɕύX
    }

    public bool IsGameEnded()
    {
        return isGameEnded;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }
}