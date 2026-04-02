using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlachBlinkEnemy : MonoBehaviour
{
    private Material blinkMaterial;
    private Material defultMaterial;
    private SpriteRenderer spriteRenderer;
    public EnemyEntity enemyEntity;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defultMaterial = spriteRenderer.material;
        blinkMaterial = Resources.Load<Material>("Materials/FlashBlink");
        enemyEntity = GetComponentInParent<EnemyEntity>();
    }
    private void OnEnable()
    {
        enemyEntity.OnTakeHit += OnEnemyAttack_OnFlashBlink;
    }
    private void OnDisable()
    {
        enemyEntity.OnTakeHit -= OnEnemyAttack_OnFlashBlink;
    }
    private void OnEnemyAttack_OnFlashBlink(object sender, System.EventArgs e)
    {
        StartCoroutine(SetBlinkingMaterial());
    }
    private IEnumerator SetBlinkingMaterial()
    {
        spriteRenderer.material = blinkMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defultMaterial;

    }

}
