using UnityEngine;

public class SpiderCrawl : MonoBehaviour
{
    public void MoveToPoint(Vector3 point)
    {
        transform.position = point;
    }
}
