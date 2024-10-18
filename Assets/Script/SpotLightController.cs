using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    public Transform playerTransform;
    public float intensity; // �X�|�b�g���C�g�̋��x
    public Color lightColor = Color.white; // �X�|�b�g���C�g�̐F

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
        // �X�|�b�g���C�g�̈ʒu���v���C���[�̓���ɐݒ�iZ��������+1.5���炷�j
        Vector3 lightPosition = playerTransform.position + Vector3.up * 10.0f + Vector3.forward * 20.0f;
        transform.position = lightPosition;

        // �X�|�b�g���C�g�̕������v���C���[�̕����Ɍ�����
        transform.LookAt(playerTransform);

        // �����v���C���[����\���ɂȂ����ꍇ�A�X�|�b�g���C�g����\���ɂ���
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