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

    private bool flipRight = true;
    private bool inGround = true;
    
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
    }
    
    void MovePlayer()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if ((moveInput > 0 && !flipRight) || (moveInput < 0 && flipRight)) Flip();

        if (enter.inTransport)
        {
            velocity = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && inGround)
        {
            velocity = jumpForce;
        }

        if (!inGround && !enter.inTransport) // add gravity
        {
            velocity += gravity * gravityScale * Time.deltaTime;
        }

        transform.Translate(new Vector3(moveInput * moveSpeed, velocity, 0f) * Time.deltaTime);

        if (transform.position.y < groundPosition && !enter.inTransport) // reset Y
        {
            transform.position = new Vector3(transform.position.x, groundPosition, transform.position.z);
            velocity = 0;
            inGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            inGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            inGround = false;
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
