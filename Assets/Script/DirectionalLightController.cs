using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    public Vector3 targetRotation; // –Ú•W‚Ì‰ñ“]Šp“x
    public float rotationSpeed; // ‰ñ“]‘¬“x

    Vector3 initialRotation; // ‰Šú‚Ì‰ñ“]Šp“x

    void Start()
    {
        initialRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        // Œ»İ‚Ì‰ñ“]Šp“x‚ğæ“¾
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // –Ú•W‚Ì‰ñ“]Šp“x‚ÉŒü‚¯‚ÄAŠÔ‚É‰‚¶‚Ä‰ñ“]‚ğs‚¤
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), step);

        // ‰ñ“]‚ª–Ú•W‚É’B‚µ‚½‚çA‰ñ“]‚ğ’â~‚·‚é
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation)) < 0.01f)
        {
            transform.rotation = Quaternion.Euler(targetRotation);
            enabled = false; // ƒXƒNƒŠƒvƒg‚ğ–³Œø‚É‚·‚é
        }
    }
}