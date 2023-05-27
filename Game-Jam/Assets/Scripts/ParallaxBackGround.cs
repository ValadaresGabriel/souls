using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    [SerializeField] float multiplier = 0.0f;
    [SerializeField] bool horizontalOnly = true;

    private float length;
    private float height;

    private Transform cameraTransform;

    private Vector3 startCameraPos;
    private Vector3 startPos;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        startCameraPos = cameraTransform.position;
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }


    private void LateUpdate()
    {
        var position = startPos;

        if (horizontalOnly) {
            position.x += multiplier * (cameraTransform.position.x - startCameraPos.x);
        }
        else {
            position += multiplier * (cameraTransform.position - startCameraPos);
        }

        transform.position = position;
    }

    private void FixedUpdate() {
        float rePosX = (1 - multiplier) * (cameraTransform.position.x - startCameraPos.x);
        if (rePosX > startPos.x + length / 2)
            startPos.x += length;
        else if (rePosX < startPos.x - length / 2)
            startPos.x -= length;    
    }
}
