using UnityEngine;
using UnityEngine.UI;

public class UIPlayerScoreController : MonoBehaviour
{
    public Transform playerTransform;
    public Text scoreText;
    int score;
    void Start()
    {
        // �����X�R�A��ݒ�
        score = 0;
        UpdateUI(); // UI��������
    }
    void Update()
    {
        // �v���C���[�̓��̏��UI��\�����邽�߂̈ʒu�X�V
        Vector3 uiPosition = Camera.main.WorldToScreenPoint(playerTransform.position + new Vector3(0, 2, 0)); // Y����2���j�b�g��ɕ\��
        scoreText.transform.position = uiPosition;
    }

    // �A�C�e�����l�����ꂽ�Ƃ��ɌĂ΂��֐�
    public void AddItem(int points)
    {
        score += points;
        UpdateUI();
    }


    // �X�R�A���擾���郁�\�b�h
    public int GetScore()
    {
        return score;
    }

    // UI���X�V����
    private void UpdateUI()
    {
        scoreText.text = score.ToString(); // �X�R�A���e�L�X�g�ɕϊ����ĕ\��
    }
}