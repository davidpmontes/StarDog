using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifespan = default;
    [SerializeField] private Pools impactEffect = default;
    [SerializeField] private float projectileVelocity = default;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 position,
                           Vector3 direction,
                           float velocityOrigin,
                           Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        rb.velocity = direction * (velocityOrigin + projectileVelocity);
        StartCoroutine(KillBulletAfterTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyDamage enemyDamage))
        {
            enemyDamage.Damage();
        }

        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        StopAllCoroutines();
        var sparks = ObjectPool.Instance.GetFromPoolInactive(impactEffect);
        sparks.SetActive(true);
        sparks.GetComponent<Sparks>().Initialize(transform.position, Quaternion.identity);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    IEnumerator KillBulletAfterTime()
    {
        yield return new WaitForSeconds(lifespan);
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
