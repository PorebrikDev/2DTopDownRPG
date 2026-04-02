using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBlink : MonoBehaviour
{
    private Material blinkMaterial;
    private Material defultMaterial;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defultMaterial = spriteRenderer.material;
        blinkMaterial = Resources.Load<Material>("Materials/FlashBlink");
    }
    private void Start()
    {
        Player.Instance.OnFlashBlink += DamageObject_OnFlashBlink;
    }
    private void OnDisable()
    {
        Player.Instance.OnFlashBlink -= DamageObject_OnFlashBlink;
    }
    private void DamageObject_OnFlashBlink(object sender, System.EventArgs e)
    {
        StartCoroutine(SetBlinkingMaterial());
    }
    private IEnumerator SetBlinkingMaterial()
    {
        spriteRenderer.material = blinkMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defultMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = blinkMaterial;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defultMaterial;
    }

}
