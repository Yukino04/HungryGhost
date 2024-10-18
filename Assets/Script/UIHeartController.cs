using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHeartController : MonoBehaviour
{
    public Image[] heartImages; // UIのハート画像
    int currentHeartIndex;

    void Start()
    {
        currentHeartIndex = heartImages.Length - 1; // 最初はすべてのハートが表示されている
    }

    // プレイヤーが障害物に当たったときにハートを減少させるメソッド
    public void DecreaseHeart()
    {
        if (currentHeartIndex >= 0)
        {
            Destroy(heartImages[currentHeartIndex].gameObject);
            currentHeartIndex--;
        }

        if (currentHeartIndex < 0)
        {
            // ハートが全て無くなった場合の処理
            FindObjectOfType<GameController>().GameOver();
        }
    }
}