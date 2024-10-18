using UnityEngine;

public class FruitController : MonoBehaviour
{
    public float rotationSpeed = 30f; // 回転速度（度/秒）
    public GameObject[] fruits; // フルーツの配列

    void Start()
    {
        // fruits配列にフルーツのGameObjectを設定する
        fruits = GameObject.FindGameObjectsWithTag("Fruit");
    }

    void Update()
    {
        // 各フルーツを回転させる
        foreach (var fruit in fruits)
        {
            // x軸方向に右回転させる
            fruit.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}