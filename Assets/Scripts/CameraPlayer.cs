using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    private Transform playerTransform;

    private Vector2 offset = new Vector2(0, 1);
    private float scrollSpeed = 100f;
    private float cameraSpeed = 0.1f;
    private float minSize = 4f;
    private float maxSize = 10f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, -10);
    }

    void FixedUpdate()
    {
        FollowPlayer();
        HandleZoom();
    }

    private void FollowPlayer()
    {
        if(playerTransform.position != transform.position)
        {
            Vector3 targetPosition = new Vector3(playerTransform.position.x + offset.x, playerTransform.position.y + offset.y, -10);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
        }
    }

    private void HandleZoom()
    {
        float scrollInput  = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput  > 0.1)
        {
            Camera.main.orthographicSize -= scrollInput + scrollSpeed * Time.deltaTime; /*Приближение*/
        }
        if (scrollInput  < -0.1)
        {
            Camera.main.orthographicSize += scrollInput + scrollSpeed * Time.deltaTime; /*Отдаление*/
        }

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize);
    }
}
