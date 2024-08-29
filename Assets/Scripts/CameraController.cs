using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    void Update()
    {
        // Rotate the camera around the center of the scene
        transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
