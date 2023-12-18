using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public bool isFacingRight = true;

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

    [Header("Camera Stuff\n")]
    [SerializeField] private float deadZoneXOffset;
    [SerializeField] private float deadZoneMinusXOffset;
    

    [Header("CheckPoint\n")]
    [SerializeField] private GameObject lastCheckpoint;

    private float horizontalMovement;
    private float lastTimeGrounded;
    private float lastTimeJumpPressed;
    private float _fallSpeedYThresholdChange;
    private float horizontalVelocity;

    private bool grounded;
    private bool invincibilityFrame;
    private bool roll;
    private bool isGamepad;

    private int jumpNumber;

    private Rigidbody2D rb;
    private CapsuleCollider2D cc2d;
    private Controles controlesScript;
    private GameObject _cameraFollow;
    private PlayerInput playerinput;
    private CameraFollowPlayer _cameraFollowObject;
    private List<GameObject> freezedObject = new List<GameObject>();
    private Animator animator;

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
        controlesScript = new Controles();
        _cameraFollow = GameObject.Find("CameraFollowPlayer");
        playerinput = GetComponent<PlayerInput>();
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

    private void Start()
    {
        Instance = this;
        walkParticle.Stop();
        rb = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CapsuleCollider2D>();
        _cameraFollowObject = _cameraFollow.GetComponent<CameraFollowPlayer>();
        _fallSpeedYThresholdChange = CameraManager.instance._fallspeedYThresholdChange;
    }

    void Update()
    {
        //print(freezedObject.Length);
        Animation();
        IsGrounded();
        if(grounded == true)
        {
            lastTimeGrounded = Time.time;

            if(controlesScript.player.roll.triggered && !roll)
            {
                roll = true;
                Invoke("StopRoll", 0.3f);
            }
            else if(roll)
            {
                cc2d.size = new Vector2(1, Mathf.Lerp(0.5f, 1.2f, 1f * Time.deltaTime));
            }
            else if(!roll && cc2d.size.y < 1.2f)
            {
                cc2d.size = new Vector2(1, Mathf.Lerp(1.2f, 0.5f, 1f * Time.deltaTime));
            }
        }
        if (controlesScript.player.jump.triggered)
        {
            lastTimeJumpPressed = Time.time;
        }
        if (controlesScript.player.jump.triggered && (lastTimeJumpPressed - lastTimeGrounded < coyoteTime || jumpNumber < 2))
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

        horizontalMovement = controlesScript.player.move.ReadValue<float>();

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
        

        //if we are falling past a certain speed threshold
        if(rb.velocity.y < _fallSpeedYThresholdChange && !CameraManager.instance.isLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        //if we are standing still or moving up
        if(rb.velocity.y >= 0f && !CameraManager.instance.isLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            //reset so it can be called again
            CameraManager.instance.LerpedFromPlayerFalling = false;
            CameraManager.instance.LerpYDamping(false);
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
        CancelInvoke("TurnCinemachine");
        if (isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = false;
            Invoke("TurnCinemachine", 0.2f);
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = true;
            Invoke("TurnCinemachine", 0.2f);
        }
    }
    
    private void TurnCinemachine()
    {
        _cameraFollowObject.CallTurn();
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

        print("Player DEAD");
    }
    
    private void Animation()
    {
        //Jumping animations
        if (controlesScript.player.jump.triggered)
        {
            animator.SetBool("isJumping", true);

        }
        if (controlesScript.player.jump.triggered && jumpNumber < 2)
        {

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


}