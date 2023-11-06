using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5f;

    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (player == null)
        {
            Debug.LogError("PlayerController não encontrado no objeto com a tag 'Player'");
        }
    }

    private void LateUpdate()
    {
        // Verifica se o jogador está morto ou se um diálogo está ativo antes de mover a câmera.
        if (player != null && !player.IsDead && !DialogManager.Instance.isDialogPlaying)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += speed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}
