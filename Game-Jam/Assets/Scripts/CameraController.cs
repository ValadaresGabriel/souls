using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 15f;

    public float smoothSpeed = 0.125f;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isDead == true || DialogManager.Instance.isDialogPlaying) return;

        Vector3 desiredPosition = new Vector3(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

}
