using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed = 1f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(speed);
    }
}
