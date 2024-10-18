using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public float speed = 10f; // �e�̑��x
    public float maxDistance = 20f; // �ő�ړ�����
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // �e��+Z�����Ɉړ�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // �ő�ړ������𒴂�����폜
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
