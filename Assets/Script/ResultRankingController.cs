using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultRankingController : MonoBehaviour
{
    public Text rankingText; // �����L���O��\������e�L�X�gUI

    void Start()
    {
        // ���݂̃X�R�A�����X�g�ɒǉ�
        ResultScoreManager.highScores.Add(ResultScoreManager.currentScore);

        // �X�R�A��傫�����Ƀ\�[�g
        ResultScoreManager.highScores.Sort((a, b) => b.CompareTo(a));

        // �����L���O��\��
        UpdateRankingUI();
    }

    // �����L���OUI���X�V����
    private void UpdateRankingUI()
    {
        rankingText.text = "Ranking\n";
        for (int i = 0; i < 5 && i < ResultScoreManager.highScores.Count; i++)
        {
            rankingText.text += (i + 1) + ". " + ResultScoreManager.highScores[i].ToString() + "\n";
        }
    }
}