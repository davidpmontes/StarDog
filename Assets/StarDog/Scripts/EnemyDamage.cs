using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer = default;
    [SerializeField] private Pools explosionEffect = default;
    [SerializeField] private int startingLife = default;
    private int life;
    private Color originalColor;
    private Color redColor;

    private void Awake()
    {
        originalColor = skinnedMeshRenderer.material.color;
        redColor = new Color(255, 0, 0);
        life = startingLife;
    }

    public void Damage()
    {
        StartCoroutine(FlashRed());
        life -= 1;
        if (life <= 0)
        {
            var explosion = ObjectPool.Instance.GetFromPoolInactive(explosionEffect);
            explosion.SetActive(true);
            explosion.GetComponent<SpecialEffect>().Initialize(transform.GetChild(0).position);
            gameObject.SetActive(false);
        }
    }

    IEnumerator FlashRed()
    {
        skinnedMeshRenderer.material.color = redColor;
        yield return new WaitForSeconds(0.1f);
        skinnedMeshRenderer.material.color = originalColor;
    }
}
