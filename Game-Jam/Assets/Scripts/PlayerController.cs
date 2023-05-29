using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float velocity;

    [SerializeField] private float respawnTime;

    [SerializeField] private bool isDead;

    private Vector2 targetPosition;

    private Rigidbody2D rb;

    private Renderer rendererExtension;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rendererExtension = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!rendererExtension.IsVisibleFrom(Camera.main))
        {
            isDead = true;
            StartCoroutine(Respawn());
        }

        if (Input.GetKey(KeyCode.Z) && isDead == false)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (rb.position != targetPosition)
        {
            Vector2 position = Vector2.MoveTowards(rb.position, targetPosition, velocity * Time.fixedDeltaTime);
            rb.MovePosition(position);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            isDead = true;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        SceneTransitionManager.Instance.StartTransition();

        yield return new WaitForSeconds(respawnTime);

        transform.position = CheckpointManager.Instance.LastCheckpoint;

        isDead = false;
    }
}
