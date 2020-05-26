using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI checkpointsScore = default;
    [SerializeField] private TextMeshProUGUI secondaryWeaponsCount = default;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCheckpointsScore(int score)
    {
        checkpointsScore.text = string.Format("{0:00}", score);
    }

    public void SetSecondaryWeaponsCount(int count)
    {
        secondaryWeaponsCount.text = string.Format("{0:00}", count);
    }
}
