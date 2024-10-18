using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText; // リザルト画面に表示するスコアのテキストUI

    void Start()
    {
        // ScoreManagerからスコアを取得して表示
        scoreText.text = "きょうのヤショクは" + ResultScoreManager.currentScore.ToString() + "コだよ";
    }
    void Update()
    {
        //ESCkeyが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //ゲームを終了
            Application.Quit();
        }
    }
}