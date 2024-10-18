using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    public Vector3 targetRotation; // �ڕW�̉�]�p�x
    public float rotationSpeed; // ��]���x

    Vector3 initialRotation; // �����̉�]�p�x

    void Start()
    {
        initialRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        // ���݂̉�]�p�x���擾
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // �ڕW�̉�]�p�x�Ɍ����āA���Ԃɉ����ĉ�]���s��
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), step);

        // ��]���ڕW�ɒB������A��]���~����
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation)) < 0.01f)
        {
            transform.rotation = Quaternion.Euler(targetRotation);
            enabled = false; // �X�N���v�g�𖳌��ɂ���
        }
    }
}