using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerFreeController : MonoBehaviour
{
    [SerializeField] private GameObject modelContainer = default;
    [SerializeField] private GameObject yaw = default;
    [SerializeField] private GameObject pitch = default;
    [SerializeField] private GameObject roll = default;

    private float minFlySpeed = 2f;
    private float maxFlySpeed = 10f;
    [SerializeField] private float flySpeed = default;

    private CharacterController cc;

    private float horizontalRaw;
    private float verticalRaw;
    private float leftTriggerRaw;
    private float rightTriggerRaw;
    private float leftShoulderRaw;
    private float rightShoulderRaw;

    private float yValue;
    private float xValue;
    private float zValue;    

    private void Awake()
    {
        cc = GetComponentInChildren<CharacterController>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        //UpdateYawPitchRollRealistic();
        UpdateYawPitchRollArcade();
        UpdateSpeed();
        MovePlane();
    }

    public Vector3 GetCCVelocity()
    {
        return cc.velocity;
    }

    private void GetInput()
    {
        horizontalRaw = Gamepad.current.leftStick.x.ReadValue();
        verticalRaw = Gamepad.current.leftStick.y.ReadValue();

        // 0 => up, 1 => held down
        leftTriggerRaw = Gamepad.current.leftTrigger.ReadValue();
        rightTriggerRaw = Gamepad.current.rightTrigger.ReadValue();

        leftShoulderRaw = Gamepad.current.leftShoulder.ReadValue();
        rightShoulderRaw = Gamepad.current.rightShoulder.ReadValue();
    }

    private void UpdateYawPitchRollRealistic()
    {
        yValue = Mathf.MoveTowards(yValue, -leftShoulderRaw + rightShoulderRaw, 10 * Mathf.Abs(yValue - (-leftShoulderRaw + rightShoulderRaw)) * Time.deltaTime);
        xValue = Mathf.MoveTowards(xValue, verticalRaw, 10 * Mathf.Abs(xValue - verticalRaw) * Time.deltaTime);
        zValue = Mathf.MoveTowards(zValue, horizontalRaw, 15 * Mathf.Abs(zValue - horizontalRaw) * Time.deltaTime);

        roll.transform.Rotate(xValue * 2f, yValue * 1f, -zValue * 3f, Space.Self);
    }

    private void UpdateYawPitchRollArcade()
    {
        yValue = Mathf.MoveTowards(yValue, -leftShoulderRaw + rightShoulderRaw, 10 * Mathf.Abs(yValue - (-leftShoulderRaw + rightShoulderRaw)) * Time.deltaTime);
        xValue = Mathf.MoveTowards(xValue, verticalRaw, 10 * Mathf.Abs(xValue - verticalRaw) * Time.deltaTime);

        var maxTurn = horizontalRaw * 2;
        zValue = Mathf.MoveTowards(zValue, maxTurn, 2 * Mathf.Abs(zValue - maxTurn) * Time.deltaTime);

        yaw.transform.Rotate(0, zValue, 0, Space.Self);
        pitch.transform.Rotate(xValue * 2f, 0, 0, Space.Self);
        roll.transform.localRotation = Quaternion.Euler(0, 0, -zValue * 40);
    }

    private void UpdateSpeed()
    {
        flySpeed = Mathf.MoveTowards(flySpeed, maxFlySpeed, Time.deltaTime * rightTriggerRaw * 10);
        flySpeed = Mathf.MoveTowards(flySpeed, minFlySpeed, Time.deltaTime * leftTriggerRaw * 10);
        CameraMovement.Instance.UpdateRadius((flySpeed - minFlySpeed) / (maxFlySpeed - minFlySpeed));
    }

    private void MovePlane()
    {
        cc.Move(pitch.transform.forward * flySpeed * Time.deltaTime);        
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.name + ", " + hit.normal);
    }
}
