using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountdownController1 : MonoBehaviour
{
    // カウントダウンを表示するためのText UIコンポーネントを設定します
    public Text countdownText;

    // GameControllerのインスタンスを保持する変数
    GameController gameController;

    void Start()
    {
        // GameControllerコンポーネントを取得します
        gameController = FindObjectOfType<GameController>();

        // 初期表示を空にします
        countdownText.text = "";
    }

    void Update()
    {
        // ゲームが終了していない場合のみカウントダウンを更新します
        if (!gameController.IsGameEnded())
        {
            // 残り時間を取得します
            float remainingTime = gameController.GetRemainingTime();

            // 残り5秒以下になったらカウントダウンを表示します
            if (remainingTime <= 5f && remainingTime > 0f)
            {
                countdownText.text = Mathf.Ceil(remainingTime).ToString();
            }
            else
            {
                // 5秒以上の場合はテキストを非表示にします
                countdownText.text = "";
            }
        }
        else
        {
            // ゲーム終了時はテキストを非表示にします
            countdownText.text = "";
        }
    }
}