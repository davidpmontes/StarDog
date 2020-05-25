using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CheckpointManager.Instance.CheckPointEntered();
        Destroy(gameObject);
    }
}
