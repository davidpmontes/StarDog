using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject cameraPositionTarget = default;
    [SerializeField]
    [Range(0, 2)]
    private float followDistance;

    void LateUpdate()
    {
        Move();    
    }

    private void Move()
    {
        transform.position = cameraPositionTarget.transform.position + new Vector3(0, 0, -followDistance);
    }
}
