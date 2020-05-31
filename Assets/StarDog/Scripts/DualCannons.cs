using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DualCannons : MonoBehaviour
{
    [SerializeField] private Transform leftBarrel = default;
    [SerializeField] private Transform rightBarrel = default;
    [SerializeField] private PlayerFreeController playerFreeController = default;

    //private bool isFiring;
    private bool isLeftFiring;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            StartFiring();

        //if (Input.GetKeyUp(KeyCode.Space))
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
            StopFiring();
    }

    public void StartFiring()
    {
        //if (isFiring)
        //    return;

        //isFiring = true;
        StartCoroutine(FireBothCannons());
    }

    public void StopFiring()
    {
        //isFiring = false;
        StopAllCoroutines();
    }

    private IEnumerator FireBothCannons()
    {
        while(true)
        {
            if (isLeftFiring)
                FireCannonLeft();
            else
                FireCannonRight();

            isLeftFiring = !isLeftFiring;
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void FireCannonLeft()
    {
        var bullet = ObjectPool.Instance.GetFromPoolInactive(Pools.BulletYellow);
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Initialize(leftBarrel.position,
                                                 transform.forward,
                                                 playerFreeController.GetCCVelocity().magnitude,
                                                 leftBarrel.rotation);
    }

    private void FireCannonRight()
    {
        var bullet = ObjectPool.Instance.GetFromPoolInactive(Pools.BulletYellow);
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Initialize(rightBarrel.position,
                                                 transform.forward,
                                                 playerFreeController.GetCCVelocity().magnitude,
                                                 rightBarrel.rotation);
    }
}
