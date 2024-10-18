using UnityEngine;
using System.Collections.Generic;

public class GameFruitManager : MonoBehaviour
{
    public GameObject[] itemPrefabs; // �A�C�e���̃v���n�u
    public MapGenerator mapGenerator; // �}�b�v�W�F�l���[�^�[
    public float[] xSpawnPositions; // �A�C�e���̐����ʒu��X�����w�肷��z��

    List<GameObject> activeItems = new List<GameObject>();
    float generationInterval = 1.0f; // �A�C�e�������̊Ԋu
    float timeSinceLastGeneration;
    GameController gameController;

    int currentXIndex = 0; // ���݂�X�ʒu�C���f�b�N�X
    int direction = 1; // X�ʒu�̕����i1�F�E�A-1�F���j

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

        // �v���C���[���ʂ�߂����A�C�e����Еt����
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

        // Y�����Œ�̈ʒu�ɐݒ�
        spawnPosition.y = 0.5f;

        // �V�����A�C�e���𐶐�
        GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.Euler(-90f, 0f, 0f));
        activeItems.Add(newItem);

        // X�ʒu�̃C���f�b�N�X���X�V
        currentXIndex += direction;
        if (currentXIndex >= xSpawnPositions.Length || currentXIndex < 0)
        {
            direction *= -1; // �����𔽓]
            currentXIndex += direction; // ���]��̈ʒu�ɏC��
        }
    }
}