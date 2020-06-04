using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }
    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckPointEntered()
    {
        score += 1;
        CanvasManager.Instance.SetCheckpointsScore(score);
    }
}
