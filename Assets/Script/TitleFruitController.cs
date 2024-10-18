using UnityEngine;

public class FruitController : MonoBehaviour
{
    public float rotationSpeed = 30f; // ��]���x�i�x/�b�j
    public GameObject[] fruits; // �t���[�c�̔z��

    void Start()
    {
        // fruits�z��Ƀt���[�c��GameObject��ݒ肷��
        fruits = GameObject.FindGameObjectsWithTag("Fruit");
    }

    void Update()
    {
        // �e�t���[�c����]������
        foreach (var fruit in fruits)
        {
            // x�������ɉE��]������
            fruit.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}