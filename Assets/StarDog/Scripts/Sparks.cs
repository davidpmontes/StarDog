using System.Collections;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    [SerializeField] private float lifespan = default;

    public void Initialize(Vector3 position,
                           Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        StartCoroutine(KillSparksAfterTime());
    }

    IEnumerator KillSparksAfterTime()
    {
        yield return new WaitForSeconds(lifespan);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
