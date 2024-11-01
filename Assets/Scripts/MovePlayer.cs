using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 2000f;
    private bool flipRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        // GetComponent<Rigidbody>().velocity = new Vector3(move * moveSpeed, GetComponent<Rigidbody>().velocity.y, 0);
        Vector2 newPos = rb.position + new Vector3(move * moveSpeed * Time.deltaTime, 0, 0);
        rb.MovePosition(newPos);

        if (move > 0 && !flipRight) Flip();
        else if (move < 0 && flipRight) Flip();
    }

    private void Flip()
    {
        flipRight = !flipRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
