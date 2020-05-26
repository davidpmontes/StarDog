﻿using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject targetPlaneObj = default;
    [SerializeField] private GameObject targetCursorObj = default;

    [SerializeField] private GameObject modelContainer = default;
    [SerializeField] private GameObject cameraPositionTarget = default;
    [SerializeField] private GameObject pitch = default;
    //[SerializeField] private GameObject roll = default;
    [SerializeField] private GameObject yaw = default;

    [SerializeField] private float maxAimingHorizontalDisplacement = default;
    [SerializeField] private float maxAimingVerticalDisplacement = default;
    [SerializeField] private float maxMovingHorizontalDisplacement = default;
    [SerializeField] private float maxMovingVerticalDisplacement = default;
    [SerializeField]
    [Range(0, 1.0f)]
    private float cameraLerpPercent = default;

    private float minFlySpeed = 0f;
    private float maxFlySpeed = 10f;
    [SerializeField] private float flySpeed = 5f;

    private float horizontalRaw;
    private float verticalRaw;
    private float leftTriggerRaw;
    private float rightTriggerRaw;

    private void Awake()
    {
        targetPlaneObj.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        MoveFrame();
        UpdateTargetCursorObj();
        UpdateTargetPlaneObj();
        MovePlane();
        UpdatePitchRollYaw();
        UpdateCameraPositionTarget();
    }

    private void GetInput()
    {
        horizontalRaw = Gamepad.current.leftStick.x.ReadValue();
        verticalRaw = Gamepad.current.leftStick.y.ReadValue();

        // 0 => up, 1 => held down
        leftTriggerRaw = Gamepad.current.leftTrigger.ReadValue();
        rightTriggerRaw = Gamepad.current.rightTrigger.ReadValue();
    }

    private void MoveFrame()
    {
        flySpeed = Mathf.MoveTowards(flySpeed, maxFlySpeed, Time.deltaTime * rightTriggerRaw * 3);
        flySpeed = Mathf.MoveTowards(flySpeed, minFlySpeed, Time.deltaTime * leftTriggerRaw * 3);

        transform.Translate(Vector3.forward * Time.deltaTime * flySpeed);
    }

    private void UpdateTargetCursorObj()
    {
        var topPadding = 0f;
        var bottomPadding = 0f;
        var leftPadding = 0f;
        var rightPadding = 0f;

        Vector3 temp = targetCursorObj.transform.localPosition;

        temp.x += horizontalRaw * Time.deltaTime * 15;
        temp.y += -verticalRaw * Time.deltaTime * 15;
        temp.z = 10;

        temp.x = Mathf.Clamp(temp.x, -maxAimingHorizontalDisplacement + leftPadding,
                                     maxAimingHorizontalDisplacement - rightPadding);
        temp.y = Mathf.Clamp(temp.y, -maxAimingVerticalDisplacement + bottomPadding,
                                     maxAimingVerticalDisplacement - topPadding);

        targetCursorObj.transform.localPosition = temp;
    }

    private void UpdateTargetPlaneObj()
    {
        var planeTarget = new Vector3(targetCursorObj.transform.localPosition.x,
                                                             targetCursorObj.transform.localPosition.y,
                                                             targetPlaneObj.transform.localPosition.z);
        planeTarget.x = Mathf.Clamp(planeTarget.x, -maxMovingHorizontalDisplacement, maxMovingHorizontalDisplacement);
        planeTarget.y = Mathf.Clamp(planeTarget.y, -maxMovingVerticalDisplacement, maxMovingVerticalDisplacement);

        targetPlaneObj.transform.localPosition = planeTarget;
    }

    private void MovePlane()
    {
        var distance = Vector3.Distance(modelContainer.transform.position, targetPlaneObj.transform.position);

        modelContainer.transform.position = Vector3.MoveTowards(modelContainer.transform.position,
                                                                targetPlaneObj.transform.position,
                                                                1.2f * distance * Time.deltaTime);
    }

    private void UpdatePitchRollYaw()
    {
        //Pitch
        var direction = (new Vector3(pitch.transform.position.x,
                                     targetCursorObj.transform.position.y,
                                     targetCursorObj.transform.position.z) - pitch.transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(direction);
        pitch.transform.localRotation = lookRotation;

        //Yaw
        direction = (new Vector3(targetCursorObj.transform.position.x,
                                 pitch.transform.position.y,
                                 targetCursorObj.transform.position.z) - yaw.transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);
        yaw.transform.localRotation = lookRotation;
    }

    private void UpdateCameraPositionTarget()
    {
        cameraPositionTarget.transform.position = Vector3.Lerp(modelContainer.transform.position, transform.position, cameraLerpPercent);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(2 * maxAimingHorizontalDisplacement, 2 * maxAimingVerticalDisplacement));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector2(2 * maxMovingHorizontalDisplacement, 2 * maxMovingVerticalDisplacement));
    }
}
