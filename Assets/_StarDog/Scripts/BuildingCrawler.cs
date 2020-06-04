using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrawler : MonoBehaviour
{
    [SerializeField] private GameObject centerPoint;
    [SerializeField] private List<GameObject> surfacePoints;
    [SerializeField] private GameObject longArm;
    [SerializeField] private GameObject emitter;
    [SerializeField] private SpiderCrawl spiderCrawl;

    private bool isMoving = true;

    private void Start()
    {
        //longArm.transform.rotation = Quaternion.identity;
        //longArm.transform.rotation = Quaternion.RotateTowards(longArm.transform.rotation, surfacePoints[0].transform.rotation, 1f);
    }

    IEnumerator SpiderMove()
    {
        while(isMoving)
        {
            //spiderCrawl.MoveToPoint();
            yield return new WaitForSeconds(1);
        }
    }

    public void AddRandomWaypoint()
    {
        longArm.transform.rotation = Quaternion.Euler(Random.Range(-90, 90), Random.Range(0, 360), 0);
        Emit();
    }

    public void Emit()
    {
        var direction = centerPoint.transform.position - emitter.transform.position;

        if (Physics.Raycast(emitter.transform.position,
                            direction, out RaycastHit hitInfo,
                            Vector3.Distance(emitter.transform.position, centerPoint.transform.position),
                            LayerMask.GetMask("buildings")))
        {            
            var waypoint = ObjectPool.Instance.GetFromPoolInactive(Pools.Waypoint);
            waypoint.transform.position = hitInfo.point;            
            waypoint.transform.LookAt(emitter.transform.position, Vector3.up);
            waypoint.SetActive(true);
            surfacePoints.Add(waypoint);
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}
