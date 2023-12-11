using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private ParticleSystem walkParticle;
    [SerializeField] float jumpForce = 6;
    private float horizontalMovement;
    private float lastTimeGrounded;
    private float lastTimeJumpPressed;
    private int grounded = 0;

    private Rigidbody2D rb;

    private void Start()
    {
        walkParticle.Stop();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if(grounded == 0)
        {
            lastTimeGrounded = Time.time;
        }
        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            lastTimeJumpPressed = Time.time;
        }

        if(horizontalMovement <= _speed && horizontalMovement >= -_speed) 
        {
            horizontalMovement += Input.GetAxis("Horizontal")/5;
        }

            if(horizontalMovement != 0)
            {
                if (horizontalMovement <= 0.05 && horizontalMovement >= -0.05)
                {
                    horizontalMovement = 0;
                }

                if (horizontalMovement < 0)
                {
                    horizontalMovement += 45 * Time.deltaTime;
                }
                else if(horizontalMovement > 0)
                {
                    horizontalMovement -= 45 * Time.deltaTime;
                }
            }
       

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && (lastTimeJumpPressed - lastTimeGrounded < 0.05f || grounded == 1))
        {
                grounded += 1;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                walkParticle.Play();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button0) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (rb.velocity.y < 0.2f && grounded > 0)
        {
            if(rb.gravityScale <= 6f)
            {
                rb.gravityScale += 20 * Time.deltaTime;
            }
        }
        else rb.gravityScale = 3f;
    }

    private void FixedUpdate()
    {
            rb.velocity = new Vector3(horizontalMovement, rb.velocity.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grounded = 1;
        }
    }
}