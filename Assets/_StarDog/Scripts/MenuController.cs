using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    private bool canMove = true;
    private int index = 1;

    [SerializeField] private GameObject[] vehicles = default;

    private void Start()
    {
        vehicles[index].transform.GetChild(0).GetChild(0).GetComponent<Animation>().Play();
        vehicles[index].transform.GetChild(0).GetComponent<Animation>().Play("MenuRaise");
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Gamepad.current == null)
            return;

        if (Gamepad.current.leftStick.x.ReadValue() < -0.5f)
        {
            if (!canMove)
                return;

            if (index > 0)
            {
                vehicles[index].transform.GetChild(0).GetChild(0).GetComponent<Animation>().Stop();
                vehicles[index].transform.GetChild(0).GetComponent<Animation>().Play("MenuLower");
                index -= 1;
                canMove = false;
                vehicles[index].transform.GetChild(0).GetChild(0).GetComponent<Animation>().Play();
                vehicles[index].transform.GetChild(0).GetComponent<Animation>().Play("MenuRaise");
            }
        }

        if (Gamepad.current.leftStick.x.ReadValue() > 0.5f)
        {
            if (!canMove)
                return;

            if (index < vehicles.Length - 1)
            {
                vehicles[index].transform.GetChild(0).GetChild(0).GetComponent<Animation>().Stop();
                vehicles[index].transform.GetChild(0).GetComponent<Animation>().Play("MenuLower");
                index += 1;
                canMove = false;
                vehicles[index].transform.GetChild(0).GetChild(0).GetComponent<Animation>().Play();
                vehicles[index].transform.GetChild(0).GetComponent<Animation>().Play("MenuRaise");
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
