using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance { get; private set; }
    [SerializeField] private GameObject cameraPositionTarget = default;
    private float radius;
    private float minRadius = 0.27f;
    private float maxRadius = 0.6f;

    private void Awake()
    {
        Instance = this;
    }

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

    public void UpdateRadius(float speedPercent)
    {
        radius = minRadius + speedPercent * (maxRadius - minRadius);
        var position = Camera.main.transform.localPosition;
        position.z = -radius;
        Camera.main.transform.localPosition = position;
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
