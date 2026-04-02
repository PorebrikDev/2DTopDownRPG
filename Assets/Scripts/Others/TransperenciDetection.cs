using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransperenciDetection : MonoBehaviour
{
    private const float FULL_NON_TRANSPARENT = 1.0f;

    [Range(0f,1f)]
    [SerializeField] private float transparencyAmount = 0.35f;
    [SerializeField] private float fadeTime = 0.5f;

    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Player>())
        {
            if (collider is CapsuleCollider2D && gameObject.activeInHierarchy)
            StartCoroutine(FadeRoutine(_spriteRenderer, fadeTime, _spriteRenderer.color.a, transparencyAmount));
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Player>() && gameObject.activeInHierarchy)
        {
            StartCoroutine(FadeRoutine(_spriteRenderer, fadeTime, _spriteRenderer.color.a, FULL_NON_TRANSPARENT));
        }
    }
    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startTransparenceAmount, float targerTransparencyAmount)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        { 
        elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startTransparenceAmount, targerTransparencyAmount, elapsedTime/fadeTime);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
    }
    }
}
