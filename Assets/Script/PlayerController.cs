using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // �ړ��֘A�̃p�����[�^
    public float forwardSpeed; // �O�i���x
    public float lateralSpeed; // ���E�ړ����x
    public float laneWidth; // ���[���̕�
    float targetX; // �ړ�����^�[�Q�b�gX���W

    // �W�����v�֘A�̃p�����[�^
    bool isJumping = false; // �W�����v�����ǂ����̃t���O
    public float jumpHeight; // �W�����v�̍���
    public float jumpSpeed; // �W�����v�̑��x
    public float jumpIncrement; // �W�����v����Y��������

    bool isControlEnabled = true; // �����L���ɂ��邩�ǂ����̃t���O

    bool isFlashing = false; // �_�Œ����ǂ����̃t���O
    public UIHeartController uiheartController; // HeartUIController�̎Q��

    // SE�֘A�̃p�����[�^
    public AudioClip seFruit;
    public AudioClip seBox;
    AudioSource audioSource;

    // �G�t�F�N�g�p��Prefab��ǉ�
    public GameObject effect01Prefab; // �t���[�c�p�G�t�F�N�g
    public GameObject effect02Prefab; // �{�b�N�X�p�G�t�F�N�g
    public GameObject jumpEffectPrefab; // �W�����v�p�G�t�F�N�g

    // �e���ˊ֘A�̃p�����[�^
    public GameObject bulletPrefab; // �e�̃v���n�u
    public float fireRate = 0.5f; // ���ˊԊu�i�b�j
    bool isFiring = false; // ���˒����ǂ����̃t���O

    void Start()
    {
        // �����̃^�[�Q�b�gX���W�͌��݂�X���W
        targetX = transform.position.x;
        uiheartController = GameObject.Find("UIHeartController").GetComponent<UIHeartController>();
        // AudioSource���擾
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isControlEnabled)
        {
            return; // ���삪�����ȏꍇ�AUpdate�𑁊��ɏI��
        }

        // �O�i
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // ���E�ړ��̓��͂��m�F
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetX -= lateralSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetX += lateralSpeed * Time.deltaTime;
        }

        // �ړ��͈͂𐧌��i��: -3����3�͈̔͂ɐ����j
        targetX = Mathf.Clamp(targetX, -laneWidth, laneWidth);

        // ���������̈ʒu�����炩�ɕύX
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * lateralSpeed);
        transform.position = newPosition;

        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jump());
        }

        // �e���ˏ���
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

        // �W�����v�G�t�F�N�g��\��
        StartCoroutine(ShowEffect(jumpEffectPrefab));

        // �㏸����
        while (transform.position.y < startY + jumpHeight)
        {
            transform.Translate(Vector3.up * jumpIncrement * Time.deltaTime * jumpSpeed);
            yield return null;
        }

        // ���~����
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
        isControlEnabled = false; // ����𖳌���
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

        // 1.5�b�ԁA0.1�b���Ƃɕ\���Ɣ�\����؂�ւ���
        float flashDuration = 1.5f;
        float flashInterval = 0.1f;
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = !renderer.enabled; // �\����Ԃ𔽓]
            }
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }

        // �_�ŏI����A�\�������ɖ߂�
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
            FlashPlayer(); // �{�b�N�X�ɓ���������_�ŏ������J�n
            this.audioSource.PlayOneShot(this.seBox);
            uiheartController.DecreaseHeart(); // �n�[�g������������
            StartCoroutine(ShowEffect(effect02Prefab)); // �G�t�F�N�g02��\��
        }
        if (other.CompareTag("Fruit") && !isFlashing)
        {
            this.audioSource.PlayOneShot(this.seFruit);
            StartCoroutine(ShowEffect(effect01Prefab)); // �G�t�F�N�g01��\��
        }
    }

    // �G�t�F�N�g��\�����A��莞�Ԍ�ɔj�󂷂�R���[�`��
    IEnumerator ShowEffect(GameObject effectPrefab)
    {
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f); // �G�t�F�N�g�̕\�����Ԃ�҂i�����ł�1.5�b�ɐݒ�j
            Destroy(effect); // �G�t�F�N�g��j��
        }
    }
}