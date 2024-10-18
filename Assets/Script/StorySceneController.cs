using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StorySceneController : MonoBehaviour
{
    public AudioClip seClip; // SE�p��AudioClip��ǉ�
    private AudioSource audioSource;
    public GameObject effectPrefab; // �G�t�F�N�g�p��Prefab��ǉ�

    void Start()
    {
        // AudioSource���擾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // �X�y�[�X�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �R���[�`�����J�n����SE���Đ����A�G�t�F�N�g��\�����A�V�[�������[�h����
            StartCoroutine(PlaySEAndShowEffectThenLoadScene());
        }
        // ESC�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �Q�[�����I��
            Application.Quit();
        }
    }

    // SE���Đ����A�G�t�F�N�g��\�����A0.3�b�҂��Ă���V�[�������[�h����R���[�`��
    private IEnumerator PlaySEAndShowEffectThenLoadScene()
    {
        if (audioSource != null && seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }

        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f); // �G�t�F�N�g�̕\�����Ԃ�҂i�����ł�1.5�b�ɐݒ�j
            Destroy(effect); // �G�t�F�N�g��j��
        }

        if (audioSource != null && seClip != null)
        {
            yield return new WaitForSeconds(seClip.length); // SE�̍Đ����Ԃ�҂�
        }

        // �񓯊��ŃV�[�������[�h����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}