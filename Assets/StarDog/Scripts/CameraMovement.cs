using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject cameraPositionTarget = default;

    void LateUpdate()
    {
        Move();    
    }

    private void Move()
    {
        transform.position = cameraPositionTarget.transform.position + new Vector3(0, 0, -1);
    }
}
