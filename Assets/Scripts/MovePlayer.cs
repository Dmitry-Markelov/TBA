using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transport transport;
    private Enter enter;

    [SerializeField] public float moveSpeed = 15f;
    private float gravity = -9.8f;
    private float gravityScale = 3f;
    private float velocity = 0f;
    private float jumpForce = 10f;
    private float groundPosition;
    private float distToGround = 1.7f;

    private bool flipRight = true;
    private bool isGrounded = true;
    
    void Awake()
    {
        transport = FindObjectOfType<Transport>();
        enter = FindObjectOfType<Enter>();
    }

    private void Start()
    {
        groundPosition = transform.position.y;
    }

    void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
        
        if (!isGrounded && !enter.inTransport) // add gravity
        {
            velocity += gravity * gravityScale * Time.deltaTime;
        }


        if (transform.position.y < groundPosition && !enter.inTransport) // reset Y
        {
            transform.position = new Vector3(transform.position.x, groundPosition, transform.position.z);
            velocity = 0;
            isGrounded = true;
        }

        GroundCheck();
    }

    private void JumpPlayer()
    {
        if (enter.inTransport)
        {
            velocity = 0;
        }
        else if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity = jumpForce;
        }
    }

    private void MovePlayer()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if ((moveInput > 0 && !flipRight) || (moveInput < 0 && flipRight)) Flip();

        transform.Translate(new Vector3(moveInput * moveSpeed, velocity, 0f) * Time.deltaTime); 
    }

    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector2.down, distToGround + 0.1f))
        {
            isGrounded = true;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }       
    }

    private void Flip()
    {
        flipRight = !flipRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
