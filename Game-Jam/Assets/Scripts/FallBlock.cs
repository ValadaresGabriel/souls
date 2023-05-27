using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    public float speed = 5f;  // Velocidade de movimento do objeto

    public bool touch = false;

    private void Update()
    {
        if(!touch){
                // Calcule a posição desejada do objeto
            Vector3 newPosition = transform.position + new Vector3(0f, -1f, 0f) * speed * Time.deltaTime;

            // Defina a nova posição do objeto
            transform.position = newPosition;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Base")){
            touch = true;
            Debug.Log("hit");   
        }
    }
}
