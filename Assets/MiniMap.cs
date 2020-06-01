using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public static MiniMap Instance { get; private set; }

    [SerializeField] private Renderer r = default;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject target;

    private float minArc = 10;
    private float maxArc = 360;

    private float maxAlpha = 0.9f;
    private float minAlpha = 0.0f;

    private float minRadius = 5f;
    private float maxRadius = 20f;

    [SerializeField]
    [Range(0, 360)]
    private float manualAngle;

    [SerializeField]
    [Range(20, 360)]
    private float arc;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // vector from player to current enemy target
        Vector3 directionToTarget = new Vector3(target.transform.position.x, 0, target.transform.position.z) -
                                    new Vector3(player.transform.position.x, 0, player.transform.position.z);

        //var angle = 180 + Vector3.SignedAngle(new Vector3(-player.transform.forward.x, 0, -player.transform.forward.z), directionToTarget, Vector3.up);

        Debug.DrawLine(player.transform.position, player.transform.position + directionToTarget * 100, Color.red);
        Debug.DrawLine(player.transform.position, player.transform.position + new Vector3(player.transform.forward.x, 0, player.transform.forward.z) * 100, Color.blue);

        // angle in degrees from player's forward direction to the directionToTarget vector
        var signedAngle = Vector3.SignedAngle(new Vector3(player.transform.forward.x, 0, player.transform.forward.z), directionToTarget, Vector3.up);

        // angle in degrees for the orange direction indicator
        var calculatedAngle = signedAngle < 0 ? -signedAngle : 360 - signedAngle;

        SetAngle(calculatedAngle);

        var distance = Vector3.Distance(new Vector3(player.transform.position.x, 0, player.transform.position.z),
                                        new Vector3(target.transform.position.x, 0, target.transform.position.z));

        if (distance > maxRadius)
        {
            SetArc(minArc);
        }
        else if (distance < minRadius)
        {
            SetArc(maxArc);
        }
        else
        {
            // 0% -> focused beam
            // 100% -> widest beam

            SetArc((1 - (distance - minRadius) / (maxRadius - minRadius)) * (maxArc - minArc) + minArc);
        }
    }

    private void SetAngle(float newAngle)
    {
        r.material.SetFloat("_Angle", newAngle);
    }

    private void SetArc(float arc)
    {
        float alpha =  maxAlpha - ((arc - minArc) / (maxArc - minArc) * (maxAlpha - minAlpha));       

        r.material.SetFloat("_Arc1", 180 - arc / 2);
        r.material.SetFloat("_Arc2", 180 - arc / 2);
        r.material.SetColor("_Color", new Color(255f / 255f, 121f / 255f, 0, alpha));
    }
}
