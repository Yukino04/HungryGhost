using UnityEngine;
using System.Collections.Generic;

public class GameFruitManager : MonoBehaviour
{
    public GameObject[] itemPrefabs; // アイテムのプレハブ
    public MapGenerator mapGenerator; // マップジェネレーター
    public float[] xSpawnPositions; // アイテムの生成位置のX軸を指定する配列

    List<GameObject> activeItems = new List<GameObject>();
    float generationInterval = 1.0f; // アイテム生成の間隔
    float timeSinceLastGeneration;
    GameController gameController;

    int currentXIndex = 0; // 現在のX位置インデックス
    int direction = 1; // X位置の方向（1：右、-1：左）

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (gameController.IsGameEnded())
        {
            return;
        }

        timeSinceLastGeneration += Time.deltaTime;

        if (timeSinceLastGeneration >= generationInterval)
        {
            GenerateItem();
            timeSinceLastGeneration = 0f;
        }

        // プレイヤーが通り過ぎたアイテムを片付ける
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            if (activeItems[i] != null && mapGenerator.playerTransform.position.z - activeItems[i].transform.position.z >= 10f)
            {
                Destroy(activeItems[i]);
                activeItems.RemoveAt(i);
            }
        }
    }

    private void GenerateItem()
    {
        Vector3 playerPosition = mapGenerator.playerTransform.position;
        float spawnX = xSpawnPositions[currentXIndex];

        Vector3 spawnPosition = new Vector3(
            spawnX,
            playerPosition.y + 0.5f,
            playerPosition.z + 15f
        );

        // Y軸を固定の位置に設定
        spawnPosition.y = 0.5f;

        // 新しいアイテムを生成
        GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.Euler(-90f, 0f, 0f));
        activeItems.Add(newItem);

        // X位置のインデックスを更新
        currentXIndex += direction;
        if (currentXIndex >= xSpawnPositions.Length || currentXIndex < 0)
        {
            direction *= -1; // 方向を反転
            currentXIndex += direction; // 反転後の位置に修正
        }
    }
}