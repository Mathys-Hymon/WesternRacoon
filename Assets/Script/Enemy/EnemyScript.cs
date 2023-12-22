using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : FreezeMasterScript
{
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private GameObject targetArmPos;
    [SerializeField] private GameObject[] ArmSR;
    [SerializeField] private LayerMask obstacle;
    [SerializeField] private float delay;
    [SerializeField] private float speed;

    private Vector2 directiontoTarget;
    private Animator anim;

    private bool canShoot = true;
    private bool isInRange;
    private bool rushPlayer;
    private bool canReach;
    private bool lookRight;
    private float floorY;


    private void Start()
    {
        IsGrounded(0);
        anim = GetComponent<Animator>();
    }

    private void EnableArm()
    {
        for(int i = 0; i < ArmSR.Length; i++)
        {
            ArmSR[i].SetActive(true);
        }
    }

    void Update()
    {
        if (!freezed)
        {
            anim.speed = 1f;
            if (rushPlayer)
            {
                if (((!lookRight && IsGrounded(1f)) || (lookRight && IsGrounded(-1f))) && CheckWall(1.5f))
                {
                    for (int i = 0; i < ArmSR.Length; i++)
                    {
                        ArmSR[i].SetActive(false);
                    }

                    anim.SetBool("Anticipate", true);
                    Invoke("Sprinting", 0.4f);
                }
                else
                {
                    CancelInvoke("Sprinting");
                    anim.SetBool("isSprinting", false);
                    anim.SetTrigger("Stop");
                    Invoke("EnableArm", 1.2f);
                    rushPlayer = false;
                    canReach = false;
                }
            }

            if(isInRange)
            {
                if (floorY >= PlayerMovement.Instance.GetFloorY() - 0.3f && floorY <= PlayerMovement.Instance.GetFloorY() + 0.3f && !rushPlayer)
                {
                    for (int i = 1; i < (int)Vector2.Distance(transform.position, PlayerMovement.Instance.transform.position) + 1; i++)
                    {
                        if (!lookRight)
                        {
                            RaycastHit2D touchPlayer = Physics2D.Raycast(transform.position + new Vector3(i, 0, 0), Vector2.down, 1.5f, obstacle);
                            if (touchPlayer.collider == null)
                            {
                                canReach = false;
                                break;
                            }
                            else
                            {
                                canReach = true;
                            }
                        }
                        else
                        {
                            RaycastHit2D touchPlayer = Physics2D.Raycast(transform.position + new Vector3(-i, 0, 0), Vector2.down, 1.5f, obstacle);
                            if (touchPlayer.collider == null)
                            {
                                canReach = false;
                                break;
                            }
                            else
                            {
                                canReach = true;
                            }
                        }
                    }
                    if(canReach)
                    {
                    float distance = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position);
                        if (CheckWall(distance))
                        {
                            rushPlayer = canReach;
                        }
                        else
                        {

                            rushPlayer = false;
                        }
                    }
                    else
                    {
                        rushPlayer = false;
                    }
                }

                if (!rushPlayer)
                {
                    float distance = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position);
                    RaycastHit2D touchPlayer = Physics2D.Raycast(transform.position, (transform.position - PlayerMovement.Instance.transform.position) * (-1), distance, obstacle);
                    Debug.DrawRay(transform.position, (transform.position - PlayerMovement.Instance.transform.position) * (-1));

                    if (transform.position.x - PlayerMovement.Instance.transform.position.x < 0 && !rushPlayer && touchPlayer.collider == null)
                    {
                        Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
                        transform.rotation = Quaternion.Euler(rotator);
                        targetArmPos.transform.position = transform.position + new Vector3(Mathf.Clamp((PlayerMovement.Instance.transform.position.x - transform.position.x)/10,-0.5f,0.5f), (PlayerMovement.Instance.transform.position.y - transform.position.y)/10, 0);
                        lookRight = false;
                    }
                    else if (transform.position.x - PlayerMovement.Instance.transform.position.x > 0 && !rushPlayer && touchPlayer.collider == null)
                    {
                        Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                        transform.rotation = Quaternion.Euler(rotator);
                        targetArmPos.transform.position = transform.position + new Vector3(Mathf.Clamp((PlayerMovement.Instance.transform.position.x - transform.position.x) / 10, -0.5f, 0.5f), (PlayerMovement.Instance.transform.position.y - transform.position.y) / 10, 0);
                        lookRight = true;
                    }

                    if (touchPlayer.collider == null && canShoot && !rushPlayer && !canReach)
                    {
                        canShoot = false;
                        Shoot();
                    }
                    else if(touchPlayer.collider != null)
                    {
                        if (lookRight)
                        {
                            targetArmPos.transform.localPosition = new Vector3(0.06f, -0.86f, 0);
                        }
                        else
                        {
                            targetArmPos.transform.localPosition = new Vector3(0.06f, -0.86f, 0);
                        }
                    }

                }
            }
            else
            {
                if(lookRight)
                {
                    targetArmPos.transform.localPosition = new Vector3(0.06f, -0.86f, 0);
                }
                else
                {
                    targetArmPos.transform.localPosition = new Vector3(0.06f, -0.86f, 0);
                }
                
            }
        }
        else if (freezed)
        {
            anim.speed = 0f;
        }
    }

    public void Shoot()
    {
        directiontoTarget = PlayerMovement.Instance.transform.position - transform.position;
        float angle = -90+Mathf.Atan2(directiontoTarget.y, directiontoTarget.x) * Mathf.Rad2Deg;
        Instantiate(bulletRef, targetArmPos.transform.position, Quaternion.Euler(0,0,angle));
        
        Invoke("ResetShoot",delay);
        
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
    
    private bool CheckWall(float Distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y-0.2f, transform.position.z), transform.right, Distance, obstacle);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), transform.right*Distance);

        if (hit.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private bool IsGrounded(float offsetX)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(offsetX,-0.5f,0), Vector2.down, 1.5f, obstacle);
        Debug.DrawRay(transform.position + new Vector3(offsetX,-0.5f,0), Vector2.down*1.5f);
        if(offsetX == 0)
        {
            floorY = hit.collider.transform.position.y;
        }
        if(hit.collider == null )
        {
            print("not grounded");
            return false;
        }
        else
        {
            return true;
        }

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }

    private void Sprinting()
    {
        if (!freezed)
        {
            for (int i = 0; i < ArmSR.Length; i++)
            {
                ArmSR[i].SetActive(false);
            }
            anim.SetBool("Anticipate", false);
            anim.SetBool("isSprinting", true);
            transform.position += transform.right * Time.deltaTime * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!freezed)
        {
            if(collision.gameObject == PlayerMovement.Instance.gameObject)
            {
                PlayerMovement.Instance.Die();
            }
        }
    }
}
