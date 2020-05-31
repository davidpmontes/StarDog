using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject cameraPositionTarget = default;
    
    void FixedUpdate()
    {
        Move();    
    }

    private void Move()
    {
        transform.position = cameraPositionTarget.transform.position;

        var targetRotation = cameraPositionTarget.transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 0.05f * Quaternion.Angle(transform.rotation, targetRotation));
    }
}
