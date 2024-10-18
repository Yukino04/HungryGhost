using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] mapPrefabs; // �}�b�v�̃v���n�u
    Queue<GameObject> activeMaps = new Queue<GameObject>();
    public int initialMapCount; // �ŏ��ɐ�������}�b�v��
    public float mapLength; // �}�b�v�̒���
    public Transform playerTransform; // �v���C���[��Transform
    Vector3 lastMapEndPosition; // �Ō�ɐ������ꂽ�}�b�v�̏I���ʒu

    void Start()
    {
        // �ŏ��̃}�b�v�̏I���ʒu��ݒ�
        lastMapEndPosition = Vector3.zero;

        // �����}�b�v�𐶐�
        for (int i = 0; i < initialMapCount; i++)
        {
            GenerateMap();
        }
    }

    void Update()
    {
        // �v���C���[���ŏ��̃}�b�v��ʂ�߂�����A�V�����}�b�v�𐶐����A�Â��}�b�v��Еt����
        if (playerTransform.position.z - mapLength > activeMaps.Peek().transform.position.z)
        {
            GenerateMap();
            Destroy(activeMaps.Dequeue());
        }
    }

    // �V�����}�b�v�𐶐�����
    private void GenerateMap()
    {
        GameObject newMap = Instantiate(mapPrefabs[Random.Range(0, mapPrefabs.Length)]);
        newMap.transform.position = lastMapEndPosition;

        // Y����0�ɌŒ肷��
        newMap.transform.position = new Vector3(newMap.transform.position.x, 0f, newMap.transform.position.z);

        // ���̃}�b�v�̏I���ʒu���X�V
        lastMapEndPosition += new Vector3(0, 0, mapLength);

        activeMaps.Enqueue(newMap);
    }
}