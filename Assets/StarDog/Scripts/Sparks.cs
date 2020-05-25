using System.Collections;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    public void Initialize(Vector3 position,
                           Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        StartCoroutine(KillSparksAfterTime());
    }

    IEnumerator KillSparksAfterTime()
    {
        yield return new WaitForSeconds(2);
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
