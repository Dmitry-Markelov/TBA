using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    private Transform playerPos;
    private UnityEngine.Vector2 offset = new UnityEngine.Vector2(0, 1);
    private float scrollSpeed = 100;
    private float cameraSpeed = 0.05f;
    private float minSize = 4;
    private float maxSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new UnityEngine.Vector3(playerPos.position.x + offset.x, playerPos.position.y + offset.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos.position != transform.position) {
            UnityEngine.Vector3 targetPosition = new UnityEngine.Vector3(playerPos.position.x + offset.x, playerPos.position.y + offset.y, -10);
            transform.position = UnityEngine.Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
        }

        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (mw > 0.1)
        {
            Camera.main.orthographicSize -= mw + scrollSpeed * Time.deltaTime; /*Приближение*/
        }
        if (mw < -0.1)
        {
            Camera.main.orthographicSize += mw + scrollSpeed * Time.deltaTime; /*Отдаление*/
        }

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize);
    }
}
