using UnityEngine;
using UnityEngine.UI;

public class UIPlayerScoreController : MonoBehaviour
{
    public Transform playerTransform;
    public Text scoreText;
    int score;
    void Start()
    {
        // 初期スコアを設定
        score = 0;
        UpdateUI(); // UIを初期化
    }
    void Update()
    {
        // プレイヤーの頭の上にUIを表示するための位置更新
        Vector3 uiPosition = Camera.main.WorldToScreenPoint(playerTransform.position + new Vector3(0, 2, 0)); // Y軸に2ユニット上に表示
        scoreText.transform.position = uiPosition;
    }

    // アイテムが獲得されたときに呼ばれる関数
    public void AddItem(int points)
    {
        score += points;
        UpdateUI();
    }


    // スコアを取得するメソッド
    public int GetScore()
    {
        return score;
    }

    // UIを更新する
    private void UpdateUI()
    {
        scoreText.text = score.ToString(); // スコアをテキストに変換して表示
    }
}