using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float velocity;

    [SerializeField] private float respawnTime;

    public bool IsDead { get; private set; }

    private Vector2 targetPosition;

    private Rigidbody2D rb;

    private Renderer rendererExtension;

    private Camera mainCamera;

    private Coroutine respawnCorroutine;

    private const float PositionTolerance = 0.1f;

    private bool isRunningRespawnCoroutine = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rendererExtension = GetComponent<Renderer>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!rendererExtension.IsVisibleFrom(mainCamera) && IsDead == false)
        {
            IsDead = true;
            if (respawnCorroutine == null)
            {
                respawnCorroutine = StartCoroutine(Respawn());
            }
        }

        if (DialogManager.Instance == null)
        {
            Debug.LogWarning("DialogManager is null!");
            return;
        }

        if (Input.GetKey(KeyCode.Z) && IsDead == false && DialogManager.Instance.isDialogPlaying == false)
        {
            targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (DialogManager.Instance.isDialogPlaying && Input.GetKeyDown(KeyCode.Z))
        {
            DialogManager.Instance.NextDialog();
        }
    }

    private void FixedUpdate()
    {
        if ((rb.position - targetPosition).sqrMagnitude > PositionTolerance * PositionTolerance)
        {
            Vector2 position = Vector2.MoveTowards(rb.position, targetPosition, velocity * Time.fixedDeltaTime);
            rb.MovePosition(position);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Obstacle") && IsDead == false)
        {
            IsDead = true;

            if (respawnCorroutine == null)
            {
                respawnCorroutine = StartCoroutine(Respawn());
            }
        }
    }

    private IEnumerator Respawn()
    {
        if (isRunningRespawnCoroutine) yield break;

        Debug.Log("Chamou Respawn");
        isRunningRespawnCoroutine = true;

        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.StartTransition();
        }

        yield return new WaitForSeconds(respawnTime);

        rb.velocity = Vector2.zero;

        if (CheckpointManager.Instance != null)
        {
            targetPosition = CheckpointManager.Instance.LastCheckpoint;
            transform.position = CheckpointManager.Instance.LastCheckpoint;
            UpdateCameraPosition();
        }

        IsDead = false;
        isRunningRespawnCoroutine = false;
        respawnCorroutine = null;
    }


    private void UpdateCameraPosition()
    {
        // float cameraWidth = mainCamera.orthographicSize * 2.0f * Screen.width / Screen.height;
        // mainCamera.transform.position = new Vector3(transform.position.x + cameraWidth / 4, transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = new Vector3(CheckpointManager.Instance.LastCheckpoint.x, CheckpointManager.Instance.LastCheckpoint.y, mainCamera.transform.position.z);
    }
}
