using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float velocity;

    private Vector2 targetPosition;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
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
}
