using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Enter enter;
    private Score score;
    private PlayerInteraction playerinteraction;

    [SerializeField] public float moveSpeed = 15f;
    private float gravity = -9.8f;
    private float gravityScale = 3f;
    private float velocity = 0f;
    private float jumpForce = 10f;
    private float groundPosition;
    private float distToGround = 1.7f;
    public float currentHealth;

    private bool flipRight = true;
    private bool isGrounded = true;
    public bool inStorm = false;
    
    void Awake()
    {
        enter = FindObjectOfType<Enter>();
        score = FindObjectOfType<Score>();
        playerinteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void Start()
    {
        groundPosition = transform.position.y;

        currentHealth = 100f;
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            DiePlayer();
        }

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
        
        moveSpeed = (inStorm) ? 7f : 15f;
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

    public void GetHealth(float value)
    {
        if (value < 0)
        {
            Debug.Log("Wrong value!");
            return;
        }

        currentHealth += value;
        currentHealth = math.clamp(currentHealth, 0, 100);
    }

    public void GetDamage(float value)
    {
        if (value < 0)
        {
            Debug.Log("Wrong value!");
            return;
        }

        currentHealth -= value;
        currentHealth = math.clamp(currentHealth, 0, 100);
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

    private void DiePlayer()
    {
        Debug.Log("Player died!");
        RestartGame();
        
    }

    private void RestartGame()
    {
        if (score.currentScore >= score.hightScore)
        {
            PlayerPrefs.SetFloat("HightScore", score.hightScore);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
