using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transport transport;
    public float moveSpeed = 2000f;
    private bool flipRight = true;

    void Awake()
    {
        transport = FindObjectOfType<Transport>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    
    void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
 
        transform.position = transform.position + new Vector3(move * moveSpeed * Time.deltaTime, 0, 0);

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
