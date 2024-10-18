using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public float speed = 10f; // ’e‚Ì‘¬“x
    public float maxDistance = 20f; // Å‘åˆÚ“®‹——£
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // ’e‚ð+Z•ûŒü‚ÉˆÚ“®
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Å‘åˆÚ“®‹——£‚ð’´‚¦‚½‚çíœ
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
