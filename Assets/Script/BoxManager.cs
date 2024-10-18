using UnityEngine;
using System.Collections.Generic;

public class BoxManager : MonoBehaviour
{
    public GameObject boxPrefab; // ボックスのプレハブ
    public MapGenerator mapGenerator; // マップジェネレーター
    public float[] xSpawnPositions; // ボックスの生成位置のX軸を指定する配列

    List<GameObject> activeBoxes = new List<GameObject>();
    float generationInterval = 1f; // ボックス生成の間隔
    float timeSinceLastGeneration;
    float currentXPosition = 0f; // ボックスの現在のX軸位置
    bool isXIncreasing = true; // X軸の増減を管理するフラグ
    float fixedYPosition = 0.5f; // ボックスの固定Y軸位置

    void Start()
    {
        // 初期ボックス生成
        GenerateBox();
    }

    void Update()
    {
        // 1秒ごとにボックスを生成する
        timeSinceLastGeneration += Time.deltaTime;
        if (timeSinceLastGeneration >= generationInterval)
        {
            GenerateBox();
            timeSinceLastGeneration = 0f;
        }

        // プレイヤーが通り過ぎたボックスを片付ける
        for (int i = activeBoxes.Count - 1; i >= 0; i--)
        {
            if (activeBoxes[i] != null && mapGenerator.playerTransform.position.z - activeBoxes[i].transform.position.z >= 10f)
            {
                Destroy(activeBoxes[i]);
                activeBoxes.RemoveAt(i);
            }
        }
    }

    // ボックスをマップの自動生成に合わせて生成する
    private void GenerateBox()
    {
        Vector3 playerPosition = mapGenerator.playerTransform.position;
        float mapLength = mapGenerator.mapLength;

        // X軸位置を順番に設定する
        if (isXIncreasing)
        {
            currentXPosition -= 1.5f;
            if (currentXPosition <= -3f)
            {
                isXIncreasing = false;
            }
        }
        else
        {
            currentXPosition += 1.5f;
            if (currentXPosition >= 3f)
            {
                isXIncreasing = true;
            }
        }

        // ボックスを生成する位置を設定
        Vector3 spawnPosition = new Vector3(
            currentXPosition, // X軸は順番に変更する位置
            fixedYPosition, // 固定のY軸位置
            playerPosition.z + mapLength + 30f // Z軸はマップの生成位置に+30した位置
        );

        // 新しいボックスを生成
        GameObject newBox = Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
        activeBoxes.Add(newBox);

        // プレイヤーとの当たり判定を追加
        newBox.AddComponent<BoxCollider>();
        newBox.GetComponent<BoxCollider>().isTrigger = true;
        newBox.tag = "Box";
    }

    // ボックスが削除されたときの処理
    public void RemoveBox(GameObject box)
    {
        if (activeBoxes.Contains(box))
        {
            activeBoxes.Remove(box);
            Destroy(box);
        }
    }
}