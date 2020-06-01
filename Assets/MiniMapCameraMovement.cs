using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject cameraPositionTarget = default;
    [SerializeField] private GameObject cameraRotationTarget = default;
    private float altitude = 100;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = new Vector3(cameraPositionTarget.transform.position.x,
                                         altitude,
                                         cameraPositionTarget.transform.position.z);
        transform.rotation = Quaternion.Euler(90, 0, -cameraRotationTarget.transform.rotation.eulerAngles.y);
    }
}
