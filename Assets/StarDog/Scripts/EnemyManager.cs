using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetNearestEnemyToPlayer()
    {
        GameObject closest = enemies.OrderBy(go => (go.transform.position - player.transform.position).sqrMagnitude)
                               .FirstOrDefault(); //Take(3)   //or use .FirstOrDefault();  if you need just one
        return closest;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
