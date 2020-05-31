using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject cameraPositionTarget = default;
    private float radius;
    
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

    public void SetRadius(float newRadius)
    {

    }

    public void LeftView()
    {
        Camera.main.transform.localPosition = new Vector3(-0.3f, 0, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(0, 90, 0);
    }

    public void RearView()
    {
        Camera.main.transform.localPosition = new Vector3(0, 0, -0.3f);
        Camera.main.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void RightView()
    {
        Camera.main.transform.localPosition = new Vector3(0.3f, 0, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(0, -90, 0);
    }

    public void TopView()
    {
        Camera.main.transform.localPosition = new Vector3(0, 0.3f, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(90, 0, 0);
    }
}
