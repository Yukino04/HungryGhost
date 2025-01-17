using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    public Vector3 targetRotation; // 目標の回転角度
    public float rotationSpeed; // 回転速度

    Vector3 initialRotation; // 初期の回転角度

    void Start()
    {
        initialRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        // 現在の回転角度を取得
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // 目標の回転角度に向けて、時間に応じて回転を行う
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), step);

        // 回転が目標に達したら、回転を停止する
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation)) < 0.01f)
        {
            transform.rotation = Quaternion.Euler(targetRotation);
            enabled = false; // スクリプトを無効にする
        }
    }
}