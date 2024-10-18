using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] mapPrefabs; // マップのプレハブ
    Queue<GameObject> activeMaps = new Queue<GameObject>();
    public int initialMapCount; // 最初に生成するマップ数
    public float mapLength; // マップの長さ
    public Transform playerTransform; // プレイヤーのTransform
    Vector3 lastMapEndPosition; // 最後に生成されたマップの終了位置

    void Start()
    {
        // 最初のマップの終了位置を設定
        lastMapEndPosition = Vector3.zero;

        // 初期マップを生成
        for (int i = 0; i < initialMapCount; i++)
        {
            GenerateMap();
        }
    }

    void Update()
    {
        // プレイヤーが最初のマップを通り過ぎたら、新しいマップを生成し、古いマップを片付ける
        if (playerTransform.position.z - mapLength > activeMaps.Peek().transform.position.z)
        {
            GenerateMap();
            Destroy(activeMaps.Dequeue());
        }
    }

    // 新しいマップを生成する
    private void GenerateMap()
    {
        GameObject newMap = Instantiate(mapPrefabs[Random.Range(0, mapPrefabs.Length)]);
        newMap.transform.position = lastMapEndPosition;

        // Y軸を0に固定する
        newMap.transform.position = new Vector3(newMap.transform.position.x, 0f, newMap.transform.position.z);

        // 次のマップの終了位置を更新
        lastMapEndPosition += new Vector3(0, 0, mapLength);

        activeMaps.Enqueue(newMap);
    }
}