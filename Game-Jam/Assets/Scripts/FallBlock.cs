using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FallBlock : MonoBehaviour
{
    public float speed = 5f;  // Velocidade de movimento do objeto

    public bool canBeDestroyed = false;

    public Vector2 startPosition;

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (canBeDestroyed)
        {
            gameObject.SetActive(false);
            return;
        }

        if (PlayerController.Instance != null)
        {
            if (PlayerController.Instance.IsDead)
            {
                transform.position = startPosition;
            }
        }

        // Calcule a posição desejada do objeto
        Vector3 newPosition = transform.position + speed * Time.deltaTime * new Vector3(0f, -1f, 0f);

        // Defina a nova posição do objeto
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("EnableCanBeDestroyed"))
        {
            canBeDestroyed = true;
        }
    }
}
