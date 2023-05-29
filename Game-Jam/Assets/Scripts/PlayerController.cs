using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float velocity;

    [SerializeField] private float respawnTime;

    [SerializeField] public bool isDead;

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
        if (!rendererExtension.IsVisibleFrom(Camera.main) && isDead == false)
        {
            isDead = true;
            StartCoroutine(Respawn());
        }

        if (Input.GetKey(KeyCode.Z) && isDead == false && DialogManager.Instance.isDialogPlaying == false)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (DialogManager.Instance.isDialogPlaying && Input.GetKeyDown(KeyCode.Z))
        {
            DialogManager.Instance.NextDialog();
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
        if (other.transform.CompareTag("Obstacle") && isDead == false)
        {
            isDead = true;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        if (isDead == false) yield break;

        Debug.Log("Chamou Respawn");

        SceneTransitionManager.Instance.StartTransition();

        yield return new WaitForSeconds(respawnTime);

        rb.velocity = Vector2.zero;
        targetPosition = CheckpointManager.Instance.LastCheckpoint;
        transform.position = CheckpointManager.Instance.LastCheckpoint;

        // Mova a posição da câmera para a posição do jogador
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

        // Ajuste a posição x da câmera com base na largura da câmera
        float cameraWidth = Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        Camera.main.transform.position = new Vector3(transform.position.x + cameraWidth / 4, Camera.main.transform.position.y, Camera.main.transform.position.z);

        isDead = false;
    }
}
