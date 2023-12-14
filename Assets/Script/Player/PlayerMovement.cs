using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public bool isFacingRight = true;

    [Header("Movement")]
    [SerializeField] private float _speed = 15f;
    [SerializeField] private ParticleSystem walkParticle;
    [SerializeField] private float groundFriction;
    [SerializeField] private float airFriction;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 6;
    [SerializeField] private float airControl = 0.8f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private LayerMask floorLayer;

    [Header("Camera Stuff")]
    [SerializeField] private GameObject _cameraFollow;
    [SerializeField] private float deadZoneXOffset;
    [SerializeField] private float deadZoneMinusXOffset;
    

    [Header("CheckPoint")]
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
    private BoxCollider2D bc2d;
    private Controles controlesScript;
    private PlayerInput playerinput;
    private CameraFollowPlayer _cameraFollowObject;


    private void Awake()
    {
        controlesScript = new Controles();
        playerinput = GetComponent<PlayerInput>();
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
        bc2d = GetComponent<BoxCollider2D>();
        _cameraFollowObject = _cameraFollow.GetComponent<CameraFollowPlayer>();
        _fallSpeedYThresholdChange = CameraManager.instance._fallspeedYThresholdChange;
    }

    void Update()
    {
        IsGrounded();
        if(grounded == true)
        {
            lastTimeGrounded = Time.time;

            if(controlesScript.player.roll.triggered && roll == false)
            {
                roll = true;
                Invoke("StopRoll", 0.2f);
            }
            else if(roll && Mathf.Abs(rb.velocity.x) > 0.1f)
            {
                bc2d.size = new Vector2(1, Mathf.Lerp(0.5f, 1f, 1f * Time.deltaTime));
            }
            else if(!roll && bc2d.size.y <= 1f)
            {
                bc2d.size = new Vector2(1, Mathf.Lerp(1f, 0.5f, 1f * Time.deltaTime));
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
            rb.velocity = new Vector3(horizontalVelocity * _speed*2, rb.velocity.y, 0);
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

        //PAS TOUCHE
        if(horizontalMovement < 0 || horizontalMovement > 0)
        {
            TurnCheck();
        }
        //C'EST BON
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



    //PAS TOUCHE
    private void TurnCheck()
    {
        if (horizontalMovement > 0 && !isFacingRight)
        {
            Turn();
        }
        else if (horizontalMovement < 0 && isFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = false;
            _cameraFollowObject.CallTurn();
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = true;

            _cameraFollowObject.CallTurn();
        }
    }
    //C'EST BON

    public void Die()
    {
        
    }


}