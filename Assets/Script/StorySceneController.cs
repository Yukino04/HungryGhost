using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StorySceneController : MonoBehaviour
{
    public AudioClip seClip; // SE用のAudioClipを追加
    private AudioSource audioSource;
    public GameObject effectPrefab; // エフェクト用のPrefabを追加

    void Start()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // スペースキーが押されたら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // コルーチンを開始してSEを再生し、エフェクトを表示し、シーンをロードする
            StartCoroutine(PlaySEAndShowEffectThenLoadScene());
        }
        // ESCキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ゲームを終了
            Application.Quit();
        }
    }

    // SEを再生し、エフェクトを表示し、0.3秒待ってからシーンをロードするコルーチン
    private IEnumerator PlaySEAndShowEffectThenLoadScene()
    {
        if (audioSource != null && seClip != null)
        {
            audioSource.PlayOneShot(seClip);
        }

        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f); // エフェクトの表示時間を待つ（ここでは1.5秒に設定）
            Destroy(effect); // エフェクトを破壊
        }

        if (audioSource != null && seClip != null)
        {
            yield return new WaitForSeconds(seClip.length); // SEの再生時間を待つ
        }

        // 非同期でシーンをロードする
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}