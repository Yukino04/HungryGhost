using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    public Transform playerTransform;
    public float intensity; // スポットライトの強度
    public Color lightColor = Color.white; // スポットライトの色

    Light spotLight;

    void Start()
    {
        spotLight = GetComponent<Light>();
        spotLight.type = LightType.Spot;
        spotLight.intensity = intensity;
        spotLight.color = lightColor;
    }

    void Update()
    {
        // スポットライトの位置をプレイヤーの頭上に設定（Z軸方向に+1.5ずらす）
        Vector3 lightPosition = playerTransform.position + Vector3.up * 10.0f + Vector3.forward * 20.0f;
        transform.position = lightPosition;

        // スポットライトの方向をプレイヤーの方向に向ける
        transform.LookAt(playerTransform);

        // もしプレイヤーが非表示になった場合、スポットライトも非表示にする
        if (!playerTransform.gameObject.activeSelf)
        {
            spotLight.enabled = false;
        }
        else
        {
            spotLight.enabled = true;
        }
    }
}