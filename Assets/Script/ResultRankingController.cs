using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultRankingController : MonoBehaviour
{
    public Text rankingText; // ランキングを表示するテキストUI

    void Start()
    {
        // 現在のスコアをリストに追加
        ResultScoreManager.highScores.Add(ResultScoreManager.currentScore);

        // スコアを大きい順にソート
        ResultScoreManager.highScores.Sort((a, b) => b.CompareTo(a));

        // ランキングを表示
        UpdateRankingUI();
    }

    // ランキングUIを更新する
    private void UpdateRankingUI()
    {
        rankingText.text = "Ranking\n";
        for (int i = 0; i < 5 && i < ResultScoreManager.highScores.Count; i++)
        {
            rankingText.text += (i + 1) + ". " + ResultScoreManager.highScores[i].ToString() + "\n";
        }
    }
}