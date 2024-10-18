using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    Vector3 offset; // �J�����̃I�t�Z�b�g
    float fixedXPosition; // �Œ肳�ꂽX���̈ʒu

    void Start()
    {
        // �����I�t�Z�b�g��ݒ� (�v���C���[�̈ʒu������3���j�b�g�A���3���j�b�g)
        offset = new Vector3(0, 2f, -5);
        // �J�����̏���X�ʒu���L��
        fixedXPosition = transform.position.x;
    }

    void LateUpdate()
    {
        // �v���C���[�̈ʒu�ɃI�t�Z�b�g���������ʒu�ɃJ������z�u
        // X���͌Œ�AY����Z���̓v���C���[�ɒǏ]
        transform.position = new Vector3(fixedXPosition, playerTransform.position.y + offset.y, playerTransform.position.z + offset.z);
    }
}