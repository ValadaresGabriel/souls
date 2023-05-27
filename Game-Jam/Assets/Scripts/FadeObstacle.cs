using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObstacle : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float fadeDuration = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float targetAlpha = 0f;

    private bool isCollided;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Fade()
    {
        if (isCollided) return;

        isCollided = true;

        StartCoroutine(FadeAlpha());
    }

    private IEnumerator FadeAlpha()
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        spriteRenderer.color = targetColor; // Garante que a cor final seja a cor alvo

        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            Fade();
        }
    }

    // private void FixedUpdate()
    // {
    //     RaycastHit2D hit = Physics2D.BoxCast(agentCollider.bounds.center + boxCastOffset, boxCastSize, 0, Vector2.zero, 0, groundMask);

    //     if (hit.collider != null)
    //     {
    //         isCollided = true;
    //         return;
    //     }

    //     isCollided = false;
    // }

    // private void OnDrawGizmos()
    // {
    //     if (agentCollider == null) return;

    //     Gizmos.color = gizmoColorNotCollided;

    //     if (isCollided)
    //     {
    //         Gizmos.color = gizmoColorCollided;
    //     }

    //     Gizmos.DrawWireCube(agentCollider.bounds.center + boxCastOffset, boxCastSize);
    // }
}
