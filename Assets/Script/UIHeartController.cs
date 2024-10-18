using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHeartController : MonoBehaviour
{
    public Image[] heartImages; // UI�̃n�[�g�摜
    int currentHeartIndex;

    void Start()
    {
        currentHeartIndex = heartImages.Length - 1; // �ŏ��͂��ׂẴn�[�g���\������Ă���
    }

    // �v���C���[����Q���ɓ��������Ƃ��Ƀn�[�g�����������郁�\�b�h
    public void DecreaseHeart()
    {
        if (currentHeartIndex >= 0)
        {
            Destroy(heartImages[currentHeartIndex].gameObject);
            currentHeartIndex--;
        }

        if (currentHeartIndex < 0)
        {
            // �n�[�g���S�Ė����Ȃ����ꍇ�̏���
            FindObjectOfType<GameController>().GameOver();
        }
    }
}