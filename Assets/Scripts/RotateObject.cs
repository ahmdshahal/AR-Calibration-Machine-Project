using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeedX = 0f;
    [SerializeField] private float rotationSpeedY = 30f; // derajat per detik
    [SerializeField] private float rotationSpeedZ = 0f;

    void Update()
    {
        transform.Rotate(rotationSpeedX * Time.deltaTime,
                         rotationSpeedY * Time.deltaTime,
                         rotationSpeedZ * Time.deltaTime);
    }
}
