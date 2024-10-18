using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    Vector3 offset; // カメラのオフセット
    float fixedXPosition; // 固定されたX軸の位置

    void Start()
    {
        // 初期オフセットを設定 (プレイヤーの位置より後ろに3ユニット、上に3ユニット)
        offset = new Vector3(0, 2f, -5);
        // カメラの初期X位置を記憶
        fixedXPosition = transform.position.x;
    }

    void LateUpdate()
    {
        // プレイヤーの位置にオフセットを加えた位置にカメラを配置
        // X軸は固定、Y軸とZ軸はプレイヤーに追従
        transform.position = new Vector3(fixedXPosition, playerTransform.position.y + offset.y, playerTransform.position.z + offset.z);
    }
}