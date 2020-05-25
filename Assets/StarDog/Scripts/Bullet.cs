using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 position,
                           Vector3 velocity,
                           Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        rb.velocity = velocity;
        StartCoroutine(KillBulletAfterTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyDamage enemyDamage))
        {
            enemyDamage.Damage();
        }
        //other.GetComponent<EnemyDamage>().Damage();
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        StopAllCoroutines();
        var sparks = ObjectPool.Instance.GetFromPoolInactive(Pools.SparksYellow);
        sparks.SetActive(true);
        sparks.GetComponent<Sparks>().Initialize(transform.position, Quaternion.identity);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }

    IEnumerator KillBulletAfterTime()
    {
        yield return new WaitForSeconds(3);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
