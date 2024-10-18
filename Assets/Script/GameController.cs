using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text timeUpText; // "Time Up"を表示するためのUI
    public Text gameOverText; // "Game Over"を表示するためのUI
    public float gameDuration; // ゲームの総時間（秒）
    float remainingTime; // 残り時間
    bool isGameEnded = false; // ゲームが終了したかどうかのフラグ

    void Start()
    {
        timeUpText.gameObject.SetActive(false); // 最初は"Time Up"を非表示にする
        gameOverText.gameObject.SetActive(false); // 最初は"Game Over"を非表示にする
        remainingTime = gameDuration; // 残り時間をゲームの総時間に設定
    }

    void Update()
    {
        //ESCkeyが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //ゲームを終了
            Application.Quit();
        }
        if (!isGameEnded)
        {
            remainingTime -= Time.deltaTime; // 残り時間を減らす
            if (remainingTime <= 0)
            {
                EndGame(); // 残り時間が0以下になったらゲーム終了処理を呼ぶ
            }
        }
        else
        {
            // ゲーム終了後、スペースキーが押されたらリザルト画面に遷移
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeToResultScene();
            }
        }
    }

    public void EndGame()
    {
        isGameEnded = true;
        remainingTime = 0; // 残り時間が0以下にいかないように設定
        timeUpText.gameObject.SetActive(true); // "Time Up"を表示する
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.DisableControl(); // プレイヤーの操作を無効化
        }
        // スコアを静的変数に保存
        ResultScoreManager.currentScore = FindObjectOfType<UIPlayerScoreController>().GetScore();
    }

    public void GameOver()
    {
        isGameEnded = true;
        gameOverText.gameObject.SetActive(true); // "Game Over"を表示する
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.DisableControl(); // プレイヤーの操作を無効化
        }
        GameFruitManager gameFruitManager = FindObjectOfType<GameFruitManager>();
        if (gameFruitManager != null)
        {
            gameFruitManager.enabled = false; // アイテム生成を停止
        }
        BoxManager boxManager = FindObjectOfType<BoxManager>();
        if (boxManager != null)
        {
            boxManager.enabled = false; // ボックス生成を停止
        }
        // スコアを静的変数に保存
        ResultScoreManager.currentScore = FindObjectOfType<UIPlayerScoreController>().GetScore();
    }

    private void ChangeToResultScene()
    {
        SceneManager.LoadScene("ResultScene"); // 指定されたリザルトシーンに変更
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