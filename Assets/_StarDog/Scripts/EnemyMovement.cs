using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 certainPoint;

    private void Awake()
    {
        certainPoint = transform.localPosition;
    }

    void Update()
    {
        UpAndDown();
    }

    private void UpAndDown()
    {
        transform.localPosition = certainPoint + new Vector3(0, Mathf.Sin(Time.time) * 2, 0);
    }
}
