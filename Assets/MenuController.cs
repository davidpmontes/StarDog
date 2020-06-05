using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    private bool canMove = true;
    private int index = 0;
    private int minIndex = 0;
    private int maxIndex = 3;

    [SerializeField] private GameObject cursorTarget = default;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Gamepad.current.leftStick.x.ReadValue() < -0.5f)
        {
            if (!canMove)
                return;

            if (index > minIndex)
            {
                cursorTarget.transform.position -= cursorTarget.transform.right * 0.25f;
                index -= 1;
                canMove = false;
            }
        }

        if (Gamepad.current.leftStick.x.ReadValue() > 0.5f)
        {
            if (!canMove)
                return;

            if (index < maxIndex)
            {
                cursorTarget.transform.position += cursorTarget.transform.right * 0.25f;
                index += 1;
                canMove = false;
            }
        }

        if (Mathf.Abs(Gamepad.current.leftStick.x.ReadValue()) < 0.5f)
        {
            canMove = true;
        }
    }

    public void OnSignalCameraDeactivate()
    {
        Debug.Log("deactivate camera");
    }
}
