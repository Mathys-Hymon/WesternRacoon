using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    
    [SerializeField] private SoundPlayer _audioPlayer;

    [Header("Movement\n")]
    [SerializeField] private float _speed = 15f;
    [SerializeField] private ParticleSystem walkParticle;
    [SerializeField] private float groundFriction;
    [SerializeField] private float airFriction;
    [SerializeField] private float dashForce = 3;

    [Header("Jump\n")]
    [SerializeField] private float jumpForce = 6;
    [SerializeField] private float airControl = 0.8f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private LayerMask floorLayer;
    
    [Header("Die\n")]
    [SerializeField] private ParticleSystem diedParticle;
    

    private float horizontalMovement;
    private float lastTimeGrounded;
    private float lastTimeJumpPressed;
    private float horizontalVelocity;

    private bool grounded;
    private bool roll;
    private bool isGamepad;
    private bool dead;
    private bool isFacingRight = true;

    private int jumpNumber;
    private int actualRoom = 1;

    private Rigidbody2D rb;
    private CircleCollider2D cc2d;
    private Controles controlesScript;
    private List<GameObject> freezedObject = new List<GameObject>();
    private Animator animator;
    private CheckPointScript checkpoint;

    public void SetFreezedObject(GameObject newObject)
    {
        if(freezedObject.Count <3)
        {
            freezedObject.Add(newObject);
        }
        else
        {
            freezedObject.Remove(freezedObject[0]);
            freezedObject.Add(newObject);
        }
    }
    public List<GameObject> GetFreezedObject()
    {
        return freezedObject;
    }
    public void DestroyFreezedObject(GameObject oldGameobject)
    {
        freezedObject.Remove(oldGameobject);
    }
    private void Awake()
    {
        Instance = this;
 
        controlesScript = new Controles();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controlesScript.Enable();
    }

    private void OnDisable()
    {
        controlesScript.Disable();
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    public int ActualRoom()
    {
        return actualRoom;
    }

    public void SetNewCheckPoint(CheckPointScript newCheckpoint)
    {
        if(checkpoint != null)
        {
            Destroy(checkpoint);
        }
        checkpoint = newCheckpoint;
    }

    public Vector3 GetCheckpoint()
    {
        return checkpoint.RespawnPosition();
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath + "/data.save");

        Time.timeScale = 1;
        walkParticle.Stop();
        rb = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Animation();
        Sound();
        IsGrounded();
        if(grounded == true)
        {
            lastTimeGrounded = Time.time;

            if(controlesScript.player.roll.triggered && !roll && !dead)
            {
                roll = true;
                Invoke("StopRoll", 0.3f);
            }
            else if(roll)
            {
                cc2d.radius = Mathf.Lerp(0.25f, 0.55f, 1f * Time.deltaTime);
                cc2d.offset = new Vector2(0,Mathf.Lerp(-0.37f, -0.1f, 1f * Time.deltaTime));
            }
            else if(!roll && cc2d.radius < 0.5f)
            {
                cc2d.radius = Mathf.Lerp(0.55f, 0.25f, 1f * Time.deltaTime);
                cc2d.offset = new Vector2(0,Mathf.Lerp(-0.1f, -0.37f, 1f * Time.deltaTime));
            }
        }
        if (controlesScript.player.jump.triggered && !dead)
        {
            lastTimeJumpPressed = Time.time;
        }
        if (controlesScript.player.jump.triggered && (lastTimeJumpPressed - lastTimeGrounded < coyoteTime || jumpNumber < 2) && !dead)
        {
            if(lastTimeJumpPressed - lastTimeGrounded > coyoteTime)
            {
                jumpNumber = 1;
            }
            
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
        if ((Input.GetKeyUp(KeyCode.Joystick1Button0) || Input.GetKeyUp(KeyCode.Space)) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if(!dead)
        {
            horizontalMovement = controlesScript.player.move.ReadValue<float>();
        }
        else
        {
            horizontalMovement = 0f;
        }

        if (horizontalMovement != 0)
        {
            horizontalVelocity = horizontalMovement;
        }
        else if (grounded)
        {
            horizontalVelocity -= (groundFriction / 10f) * horizontalVelocity;
        }
        else
        {
            horizontalVelocity -= (airFriction / 10f) * horizontalVelocity;
        }

        if (rb.velocity.y < 0.2f && !grounded)
        {
            rb.gravityScale += 20 * Time.deltaTime;
        }
        else
        {
            rb.gravityScale = 3f;
        }

        if (controlesScript.player.unfreeze.triggered)
        {
            for (int i = 0; i < freezedObject.Count; i++)
            {
                freezedObject[i].GetComponent<FreezeMasterScript>().ResetTimer();
            }
            freezedObject.Clear();
        }

    }
    private void StopRoll()
    {
        roll = false;
    }

    private void FixedUpdate()
    {
        if(roll)
        {
            if(Mathf.Abs(horizontalVelocity) >= 0.1f)
            {
                rb.velocity = new Vector3(Mathf.Clamp(horizontalVelocity * _speed * dashForce, -30, 30), rb.velocity.y, 0);
            }
            else
            {
                if (isFacingRight)
                {
                    rb.velocity = new Vector2(10 * dashForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-10 * dashForce, rb.velocity.y);
                }
            }
        }
        else
        {
            if(grounded)
            {
                rb.velocity = new Vector3(horizontalVelocity * _speed, rb.velocity.y, 0);
            }
            else
            {
                rb.velocity = new Vector3(horizontalVelocity * _speed * airControl, rb.velocity.y, 0);
            }
        } 
        TurnCheck();
    }

    private void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, floorLayer);
        
        //print(hit.collider.gameObject.name);
        if (hit.collider != null)
        {
            jumpNumber = 0;
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public float GetFloorY()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, floorLayer);
        if (hit.collider != null)
        {
            return hit.collider.transform.position.y;
        }
        else
        {
            return 1000000f;
        }
    }
    private void TurnCheck()
    {
        if (isGamepad)
        {
            if (controlesScript.player.aim.ReadValue<Vector2>().x > 0 && !isFacingRight)
            {
                Turn();
            }
            else if (controlesScript.player.aim.ReadValue<Vector2>().x < 0 && isFacingRight)
            {
                Turn();
            }
            else if(controlesScript.player.aim.ReadValue<Vector2>().x == 0)
            {
                if(horizontalMovement > 0 && !isFacingRight)
                {
                    Turn();
                }
                else if (horizontalMovement < 0 && isFacingRight)
                {
                    Turn();
                }
            }
        }
        else
        {
            if(transform.position.x - Camera.main.ScreenToWorldPoint(controlesScript.player.aim.ReadValue<Vector2>()).x < 0 && !isFacingRight)
            {
                Turn();
            }
            else if(transform.position.x - Camera.main.ScreenToWorldPoint(controlesScript.player.aim.ReadValue<Vector2>()).x > 0 && isFacingRight)
            {
                Turn();
            }
        }
    }

    private void Turn()
    {
        if (isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = false;
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = true;
        }
    }

    public bool getGrounded()
    {
        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Die();
            
        }
    }

    public void Die()
    {
        if(!dead)
        {
            dead = true;
            _audioPlayer.PlayAudio(SoundFX.Death);
            diedParticle.Play();
            CameraScript.Instance.Shake(1f, 0.4f, 0.5f);
            Respawn(transform.position);
        }
    }

    private void Respawn(Vector3 oldPosition)
    {
        transform.position = new Vector3(checkpoint.RespawnPosition().x, checkpoint.RespawnPosition().y, transform.position.z);
        diedParticle.transform.position = oldPosition;
        for (int i = 0; i < freezedObject.Count; i++)
        {
            freezedObject[i].GetComponent<FreezeMasterScript>().ResetTimer();
        }
        freezedObject.Clear();
        Invoke("ResetParticle",1f);
    }
    
    private void ResetParticle()
    {
        diedParticle.transform.position = transform.position;
        dead = false;
    }


    private void Animation()
    {
        //Jumping animations
        if (controlesScript.player.jump.triggered)
        {
            animator.SetBool("isJumping", true);
        }
        if (controlesScript.player.jump.triggered && !grounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetTrigger("DoubleJumping");
        }

        //Moving animations
        if (horizontalMovement > 0 && isFacingRight)
        {
            animator.SetBool("RunForward", true);
            animator.SetBool("RunBackward", false);
        }
        else if (horizontalMovement < 0 && !isFacingRight)
        {
            animator.SetBool("RunForward", true);
            animator.SetBool("RunBackward", false);
        }
        else
        {
            animator.SetBool("RunForward", false);
            animator.SetBool("RunBackward", true);
        }
        if (horizontalMovement == 0|| !grounded)
        {
            animator.SetBool("RunForward", false);
            animator.SetBool("RunBackward", false);

        }

        if (grounded)
        {
            animator.SetBool("Falling", false);
            animator.ResetTrigger("DoubleJumping");
        }

        //Falling animation
        if (rb.velocity.y < 0.2f && !grounded)
        {
            animator.SetBool("Falling", true);
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("Falling", false);
        }
        
        //Rolling animation
        if(controlesScript.player.roll.triggered && !roll)
        {
            animator.SetBool("isRolling", true);
        }
        else if(!roll)
        {
            animator.SetBool("isRolling", false);
        }
    }

    private void Sound()
    {
        if (controlesScript.player.jump.triggered && grounded)
        {
            _audioPlayer.PlayAudio(SoundFX.Jump);
        }

        if (controlesScript.player.jump.triggered && !grounded)
        {
            _audioPlayer.PlayAudio(SoundFX.DoubleJump);
        }
        
        if (controlesScript.player.roll.triggered && grounded)
        {
            _audioPlayer.PlayAudio(SoundFX.Roll);
        }
    
        if (controlesScript.player.shoot.triggered)
        {
            _audioPlayer.PlayAudio(SoundFX.Shoot);
        }
        
    }
}