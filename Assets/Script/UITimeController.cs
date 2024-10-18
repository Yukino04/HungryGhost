using UnityEngine;
using UnityEngine.UI;

public class UITimeController : MonoBehaviour
{
    public Text timeText;
    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        float remainingTime = gameController.GetRemainingTime();
        timeText.text = Mathf.Max(remainingTime, 0).ToString("F1");
    }
}