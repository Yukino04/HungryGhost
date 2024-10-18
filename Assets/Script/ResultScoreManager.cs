using System.Collections.Generic;

public static class ResultScoreManager
{
    public static int currentScore; // ゲーム終了時のスコアを保持する静的変数
    public static List<int> highScores; // 実行中のランキングを保持するリスト
    static bool initialized = false; // 初期化済みかどうかのフラグ

    // ランキングリストを初期化するメソッド
    public static void Initialize()
    {
        if (!initialized)
        {
            highScores = new List<int>();
            initialized = true;
        }
    }
}