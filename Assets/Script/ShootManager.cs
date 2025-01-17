using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public float speed = 10f; // 弾の速度
    public float maxDistance = 20f; // 最大移動距離
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 弾を+Z方向に移動
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // 最大移動距離を超えたら削除
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
