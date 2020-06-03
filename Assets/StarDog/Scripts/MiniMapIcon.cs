using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    [SerializeField] private Pools poolIcon;
    private GameObject icon;

    private void Start()
    {
        icon = ObjectPool.Instance.GetFromPoolInactive(poolIcon);
        icon.SetActive(true);
    }

    public void SetIconYellow()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(icon);
        icon = ObjectPool.Instance.GetFromPoolInactive(Pools.MiniMapYellowDot);
        icon.SetActive(true);
    }

    private void OnDisable()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(icon);
    }

    private void FixedUpdate()
    {
        icon.transform.position = new Vector3(transform.position.x,
                                              0,
                                              transform.position.z);
    }
}
