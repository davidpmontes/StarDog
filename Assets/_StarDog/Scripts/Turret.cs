using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float trackingRadius;

    private void Update()
    {
        Aim();
        Shoot();
    }

    private void Aim()
    {
        var distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < trackingRadius)
        {
            var lookRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
            var angle = Quaternion.Angle(transform.rotation, lookRotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 2 * angle * Time.deltaTime);
        }
        else
        {
            var angle = Quaternion.Angle(transform.rotation, Quaternion.identity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 2 * angle * Time.deltaTime);
        }
    }

    private void Shoot()
    {

    }
}
