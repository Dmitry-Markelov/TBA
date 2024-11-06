using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transport transport;

    [SerializeField] public float moveSpeed = 15f;
    private bool flipRight = true;

    void Awake()
    {
        transport = FindObjectOfType<Transport>();
    }

    void FixedUpdate()
    {
        Move();
    }
    
    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
 
        transform.position += new Vector3(moveInput * moveSpeed * Time.deltaTime, 0f, 0f);

        if ((moveInput > 0 && !flipRight) || (moveInput < 0 && flipRight)) Flip();
    }

    private void Flip()
    {
        flipRight = !flipRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
