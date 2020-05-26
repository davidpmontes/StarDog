using UnityEngine;
using UnityEngine.InputSystem;

public class MissileLauncher : MonoBehaviour
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
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            Fire();
        }
    }

    private void UpdateVelocity()
    {
        currVelocity = transform.position - lastPosition;
        lastPosition = transform.position;
    }

    public void Fire()
    {
        if (isLeftFiring)
            FireCannonLeft();
        else
            FireCannonRight();

        isLeftFiring = !isLeftFiring;
    }

    private void FireCannonLeft()
    {
        var projectile = ObjectPool.Instance.GetFromPoolInactive(Pools.RocketOrange);
        projectile.SetActive(true);
        projectile.GetComponent<Bullet>().Initialize(leftBarrel.position,
                                                     currVelocity + leftBarrel.forward * 25,
                                                     leftBarrel.rotation);
    }

    private void FireCannonRight()
    {
        var projectile = ObjectPool.Instance.GetFromPoolInactive(Pools.RocketOrange);
        projectile.SetActive(true);
        projectile.GetComponent<Bullet>().Initialize(rightBarrel.position,
                                                     currVelocity + rightBarrel.forward * 25,
                                                     rightBarrel.rotation);
    }
}
