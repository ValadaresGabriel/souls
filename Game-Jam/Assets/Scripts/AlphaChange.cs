using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChange : MonoBehaviour
{
    public float fadeDuration = 2f; // Duração da transição de fade
    public float targetAlpha = 1f; // Valor alpha final desejado

    public GameObject target;
    public GameObject startPoint;


    private SpriteRenderer spriteRenderer;

    private Coroutine myCoroutine;

    private void Start()
    {
        spriteRenderer = target.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log($"Target: {target.transform.position.x}");
        Debug.Log($"Ponto: {startPoint.transform.position.x}");
        if (target.transform.position.x >= startPoint.transform.position.x)
        {
            Debug.Log("Start");
            myCoroutine = StartCoroutine(FadeAlpha());
        }
        if(spriteRenderer.color.a == targetAlpha && myCoroutine != null){
            Debug.Log("para");
            StopCoroutine(myCoroutine);
            gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator FadeAlpha()
    {
        float elapsedTime = 0f;
        Color currentColor;

        currentColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            Color newColor = Color.Lerp(currentColor, new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha), t);
            spriteRenderer.color = newColor;
            yield return null;
        }
    }
}
