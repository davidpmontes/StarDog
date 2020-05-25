using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DualCannons : MonoBehaviour
{
    [SerializeField] private Transform leftBarrel = default;
    [SerializeField] private Transform rightBarrel = default;

    private bool isLeftFiring;
    private Vector3 lastPosition;
    private Vector3 currVelocity;

    private void Awake()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        GetInput();
        UpdateVelocity();
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

    private void UpdateVelocity()
    {
        currVelocity = transform.position - lastPosition;
        lastPosition = transform.position;
    }

    public void StartFiring()
    {
        StartCoroutine(FireBothCannons());
    }

    public void StopFiring()
    {
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
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void FireCannonLeft()
    {
        var bullet = ObjectPool.Instance.GetFromPoolInactive(Pools.BulletYellow);
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Initialize(leftBarrel.position,
                                                 currVelocity + leftBarrel.forward * 25,
                                                 leftBarrel.rotation);
    }

    private void FireCannonRight()
    {
        var bullet = ObjectPool.Instance.GetFromPoolInactive(Pools.BulletYellow);
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Initialize(rightBarrel.position,
                                                 currVelocity + rightBarrel.forward * 25,
                                                 rightBarrel.rotation);
    }
}
