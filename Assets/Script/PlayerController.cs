using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // 移動関連のパラメータ
    public float forwardSpeed; // 前進速度
    public float lateralSpeed; // 左右移動速度
    public float laneWidth; // レーンの幅
    float targetX; // 移動するターゲットX座標

    // ジャンプ関連のパラメータ
    bool isJumping = false; // ジャンプ中かどうかのフラグ
    public float jumpHeight; // ジャンプの高さ
    public float jumpSpeed; // ジャンプの速度
    public float jumpIncrement; // ジャンプ時のY軸増加量

    bool isControlEnabled = true; // 操作を有効にするかどうかのフラグ

    bool isFlashing = false; // 点滅中かどうかのフラグ
    public UIHeartController uiheartController; // HeartUIControllerの参照

    // SE関連のパラメータ
    public AudioClip seFruit;
    public AudioClip seBox;
    AudioSource audioSource;

    // エフェクト用のPrefabを追加
    public GameObject effect01Prefab; // フルーツ用エフェクト
    public GameObject effect02Prefab; // ボックス用エフェクト
    public GameObject jumpEffectPrefab; // ジャンプ用エフェクト

    // 弾発射関連のパラメータ
    public GameObject bulletPrefab; // 弾のプレハブ
    public float fireRate = 0.5f; // 発射間隔（秒）
    bool isFiring = false; // 発射中かどうかのフラグ

    void Start()
    {
        // 初期のターゲットX座標は現在のX座標
        targetX = transform.position.x;
        uiheartController = GameObject.Find("UIHeartController").GetComponent<UIHeartController>();
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isControlEnabled)
        {
            return; // 操作が無効な場合、Updateを早期に終了
        }

        // 前進
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 左右移動の入力を確認
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetX -= lateralSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetX += lateralSpeed * Time.deltaTime;
        }

        // 移動範囲を制限（例: -3から3の範囲に制限）
        targetX = Mathf.Clamp(targetX, -laneWidth, laneWidth);

        // 水平方向の位置を滑らかに変更
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * lateralSpeed);
        transform.position = newPosition;

        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jump());
        }

        // 弾発射処理
        if (Input.GetKeyDown(KeyCode.Z) && !isFiring)
        {
            isFiring = true;
            StartCoroutine(FireBullets());
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isFiring = false;
        }
    }

    private IEnumerator FireBullets()
    {
        while (isFiring)
        {
            FireBullet();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void FireBullet()
    {
        Vector3 bulletSpawnPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Instantiate(bulletPrefab, bulletSpawnPosition, transform.rotation);
    }

    IEnumerator Jump()
    {
        isJumping = true;
        float startY = transform.position.y;

        // ジャンプエフェクトを表示
        StartCoroutine(ShowEffect(jumpEffectPrefab));

        // 上昇処理
        while (transform.position.y < startY + jumpHeight)
        {
            transform.Translate(Vector3.up * jumpIncrement * Time.deltaTime * jumpSpeed);
            yield return null;
        }

        // 下降処理
        while (transform.position.y > startY)
        {
            transform.Translate(Vector3.down * jumpIncrement * Time.deltaTime * jumpSpeed);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        isJumping = false;
    }

    public void DisableControl()
    {
        isControlEnabled = false; // 操作を無効化
    }

    public void FlashPlayer()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    IEnumerator FlashRoutine()
    {
        isFlashing = true;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        // 1.5秒間、0.1秒ごとに表示と非表示を切り替える
        float flashDuration = 1.5f;
        float flashInterval = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = !renderer.enabled; // 表示状態を反転
            }
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }

        // 点滅終了後、表示を元に戻す
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        isFlashing = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") && !isFlashing)
        {
            FlashPlayer(); // ボックスに当たったら点滅処理を開始
            this.audioSource.PlayOneShot(this.seBox);
            uiheartController.DecreaseHeart(); // ハートを減少させる
            StartCoroutine(ShowEffect(effect02Prefab)); // エフェクト02を表示
        }
        if (other.CompareTag("Fruit") && !isFlashing)
        {
            this.audioSource.PlayOneShot(this.seFruit);
            StartCoroutine(ShowEffect(effect01Prefab)); // エフェクト01を表示
        }
    }

    // エフェクトを表示し、一定時間後に破壊するコルーチン
    IEnumerator ShowEffect(GameObject effectPrefab)
    {
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f); // エフェクトの表示時間を待つ（ここでは1.5秒に設定）
            Destroy(effect); // エフェクトを破壊
        }
    }
}