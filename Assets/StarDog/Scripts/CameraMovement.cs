using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance { get; private set; }
    [SerializeField] private GameObject externalTarget = default;
    [SerializeField] private GameObject cockpitTarget = default;
    private float radius;
    private float minRadius = 0.27f;
    private float maxRadius = 0.6f;

    delegate void MoveCamera();
    MoveCamera moveCamera;

    private bool isCockpit = false;

    private void Awake()
    {
        Instance = this;

        if (isCockpit)
            moveCamera = MoveCockpit;
        else
            moveCamera = MoveExternal;
    }

    void FixedUpdate()
    {
        moveCamera.Invoke();    
    }

    private void MoveCockpit()
    {
        transform.position = cockpitTarget.transform.position;
        transform.rotation = cockpitTarget.transform.rotation;
    }

    private void MoveExternal()
    {
        RearView();
        transform.position = externalTarget.transform.position;

        var targetRotation = externalTarget.transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 0.05f * Quaternion.Angle(transform.rotation, targetRotation));
    }

    public void UpdateRadius(float speedPercent)
    {
        if (isCockpit)
        {
            Camera.main.transform.localPosition = Vector3.zero;
            return;
        }

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
        Camera.main.transform.localPosition = new Vector3(0, 0.1f, -0.3f);
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

    public void Cockpit()
    {
        moveCamera = MoveCockpit;
        isCockpit = true;
    }

    public void External()
    {
        moveCamera = MoveExternal;
        isCockpit = false;
    }
}
