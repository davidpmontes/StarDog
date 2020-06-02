using System.Collections;
using UnityEngine;

public class SpecialEffect : MonoBehaviour
{
    [SerializeField] private float lifespan = default;

    public void Initialize(Vector3 position)
    {
        Initialize(position, Quaternion.identity);
    }

    public void Initialize(Vector3 position,
                           Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        StartCoroutine(StopAfterTime());
    }

    IEnumerator StopAfterTime()
    {
        yield return new WaitForSeconds(lifespan);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
