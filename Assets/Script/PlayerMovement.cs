using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    [SerializeField] private float _speed = 15f;
    [SerializeField] private ParticleSystem walkParticle;
    [SerializeField] private float jumpForce = 6;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private CameraMovement cameraRef;
    [SerializeField] private GameObject lastCheckpoint;

    private float horizontalMovement;
    private float lastTimeGrounded;
    private float lastTimeJumpPressed;

    private bool grounded;
    private bool invincibilityFrame;
    private bool roll;

    private int jumpNumber;

    private Rigidbody2D rb;

    private void Start()
    {
        Instance = this;
        walkParticle.Stop();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if(grounded == true)
        {
            lastTimeGrounded = Time.time;

            if(Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button1) && roll == false)
            {
                roll = true;
                cameraRef.SetSmoothSpeed(1);
                Invoke("StopRoll", 0.2f);
            }

        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space))
        {
            lastTimeJumpPressed = Time.time;
        }
        if ((Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space)) && (lastTimeJumpPressed - lastTimeGrounded < coyoteTime || jumpNumber < 2))
        {
            jumpNumber += 1;
            if (roll)
            {
                rb.velocity = new Vector2(rb.velocity.x * 2f, jumpForce * 1.1f);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            walkParticle.Play();
        }

        if (horizontalMovement <= _speed && horizontalMovement >= -_speed && Input.GetAxis("Horizontal") != 0)
        {
             horizontalMovement += Input.GetAxis("Horizontal") / 5;         //Change to input action
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            horizontalMovement = 0;
        }
        else if (horizontalMovement < -_speed)
        {
            horizontalMovement = -_speed;
        }
        else { horizontalMovement = _speed; }

        if(lastTimeJumpPressed - lastTimeGrounded > coyoteTime && jumpNumber == 0)
        {
            jumpNumber = 2;
        }

        if ((Input.GetKeyUp(KeyCode.Joystick1Button0) || Input.GetKeyUp(KeyCode.Space)) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (rb.velocity.y < 0.2f && grounded == true)
        {
            if(rb.gravityScale <= 6f)
            {
                rb.gravityScale += 20 * Time.deltaTime;
            }
        }
        else rb.gravityScale = 3f;


    }
    private void StopRoll()
    {
        roll = false;
        cameraRef.SetSmoothSpeed(3);
    }

    private void FixedUpdate()
    {
        if(roll)
        {
            rb.velocity = new Vector3(horizontalMovement*2, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(horizontalMovement, rb.velocity.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = true;
            jumpNumber = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = false;
        }
    }

    public void Die()
    {
        
    }
}