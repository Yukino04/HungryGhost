using UnityEngine;

public class GameFruitController : MonoBehaviour
{
    public enum ItemType { Type1 = 1, Type2 = 2, Type3 = 3 }
    public ItemType itemType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            // �A�C�e�����l�������Ƃ��̏���
            FindObjectOfType<UIPlayerScoreController>().AddItem((int)itemType);
            Destroy(gameObject); // �A�C�e����j��
        }
    }
}