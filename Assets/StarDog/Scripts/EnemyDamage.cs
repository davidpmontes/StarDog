using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer = default;
    private Color originalColor;
    private Color redColor;

    private void Awake()
    {
        originalColor = skinnedMeshRenderer.material.color;
        redColor = new Color(255, 0, 0);
    }

    public void Damage()
    {
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        skinnedMeshRenderer.material.color = redColor;
        yield return new WaitForSeconds(0.1f);
        skinnedMeshRenderer.material.color = originalColor;
    }
}
