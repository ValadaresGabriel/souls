using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 0.125f;

    private void LateUpdate() {
        Vector3 desiredPosition = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        transform.position = desiredPosition;
    }
}
