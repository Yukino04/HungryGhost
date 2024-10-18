using UnityEngine;
using System.Collections.Generic;

public class BoxManager : MonoBehaviour
{
    public GameObject boxPrefab; // �{�b�N�X�̃v���n�u
    public MapGenerator mapGenerator; // �}�b�v�W�F�l���[�^�[
    public float[] xSpawnPositions; // �{�b�N�X�̐����ʒu��X�����w�肷��z��

    List<GameObject> activeBoxes = new List<GameObject>();
    float generationInterval = 1f; // �{�b�N�X�����̊Ԋu
    float timeSinceLastGeneration;
    float currentXPosition = 0f; // �{�b�N�X�̌��݂�X���ʒu
    bool isXIncreasing = true; // X���̑������Ǘ�����t���O
    float fixedYPosition = 0.5f; // �{�b�N�X�̌Œ�Y���ʒu

    void Start()
    {
        // �����{�b�N�X����
        GenerateBox();
    }

    void Update()
    {
        // 1�b���ƂɃ{�b�N�X�𐶐�����
        timeSinceLastGeneration += Time.deltaTime;
        if (timeSinceLastGeneration >= generationInterval)
        {
            GenerateBox();
            timeSinceLastGeneration = 0f;
        }

        // �v���C���[���ʂ�߂����{�b�N�X��Еt����
        for (int i = activeBoxes.Count - 1; i >= 0; i--)
        {
            if (activeBoxes[i] != null && mapGenerator.playerTransform.position.z - activeBoxes[i].transform.position.z >= 10f)
            {
                Destroy(activeBoxes[i]);
                activeBoxes.RemoveAt(i);
            }
        }
    }

    // �{�b�N�X���}�b�v�̎��������ɍ��킹�Đ�������
    private void GenerateBox()
    {
        Vector3 playerPosition = mapGenerator.playerTransform.position;
        float mapLength = mapGenerator.mapLength;

        // X���ʒu�����Ԃɐݒ肷��
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

        // �{�b�N�X�𐶐�����ʒu��ݒ�
        Vector3 spawnPosition = new Vector3(
            currentXPosition, // X���͏��ԂɕύX����ʒu
            fixedYPosition, // �Œ��Y���ʒu
            playerPosition.z + mapLength + 30f // Z���̓}�b�v�̐����ʒu��+30�����ʒu
        );

        // �V�����{�b�N�X�𐶐�
        GameObject newBox = Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
        activeBoxes.Add(newBox);

        // �v���C���[�Ƃ̓����蔻���ǉ�
        newBox.AddComponent<BoxCollider>();
        newBox.GetComponent<BoxCollider>().isTrigger = true;
        newBox.tag = "Box";
    }

    // �{�b�N�X���폜���ꂽ�Ƃ��̏���
    public void RemoveBox(GameObject box)
    {
        if (activeBoxes.Contains(box))
        {
            activeBoxes.Remove(box);
            Destroy(box);
        }
    }
}