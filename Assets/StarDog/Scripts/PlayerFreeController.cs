using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerFreeController : MonoBehaviour
{
    [SerializeField] private GameObject modelContainer = default;
    [SerializeField] private GameObject yaw = default;
    [SerializeField] private GameObject pitchRoll = default;

    private float minFlySpeed = 0f;
    private float maxFlySpeed = 20f;
    [SerializeField] private float flySpeed = 5f;

    private CharacterController cc;

    private float horizontalRaw;
    private float verticalRaw;
    private float leftTriggerRaw;
    private float rightTriggerRaw;
    private float leftShoulderRaw;
    private float rightShoulderRaw;

    private float yawValue;
    private float pitchValue;
    private float rollValue;

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
        UpdateYawPitchRollValues();
        UpdateYawPitchRoll();
        MovePlane();
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

    private void UpdateYawPitchRollValues()
    {
        yawValue = Mathf.MoveTowards(yawValue, -leftShoulderRaw + rightShoulderRaw, 10 * Mathf.Abs(yawValue - (-leftShoulderRaw + rightShoulderRaw)) * Time.deltaTime);
        pitchValue = Mathf.MoveTowards(pitchValue, verticalRaw, 10 * Mathf.Abs(pitchValue - verticalRaw) * Time.deltaTime);
        rollValue = Mathf.MoveTowards(rollValue, horizontalRaw, 15 * Mathf.Abs(rollValue - horizontalRaw) * Time.deltaTime);
    }

    private void UpdateYawPitchRoll()
    {
        yaw.transform.Rotate(0, yawValue * 1f, 0, Space.Self);
        pitchRoll.transform.Rotate(pitchValue * 2f, 0, 0, Space.Self);
        pitchRoll.transform.Rotate(0, 0, -rollValue * 2f, Space.Self);
    }

    private void MovePlane()
    {
        flySpeed = Mathf.MoveTowards(flySpeed, maxFlySpeed, Time.deltaTime * rightTriggerRaw * 10);
        flySpeed = Mathf.MoveTowards(flySpeed, minFlySpeed, Time.deltaTime * leftTriggerRaw * 10);

        //transform.Translate(pitchRoll.transform.forward * flySpeed * Time.deltaTime);
        cc.Move(pitchRoll.transform.forward * flySpeed * Time.deltaTime);
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.name + ", " + hit.normal);
    }
}
