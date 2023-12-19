using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : FreezeMasterScript
{
    [SerializeField] private GameObject bulletRef;
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

    void Update()
    {
        if (!freezed)
        {
            anim.speed = 1f;
            if (rushPlayer)
            {
                if (((!lookRight && IsGrounded(1f)) || (lookRight && IsGrounded(-1f))) && CheckWall() )
                {
                    anim.SetBool("Anticipate", true);
                    Invoke("Sprinting", 0.4f);
                }
                else
                {
                    CancelInvoke("Sprinting");
                    anim.SetBool("isSprinting", false);
                    anim.SetTrigger("Stop");
                    rushPlayer = false;
                    canReach = false;
                }
            }

            if(isInRange)
            {
                if (floorY >= PlayerMovement.Instance.GetFloorY() - 0.2f && floorY <= PlayerMovement.Instance.GetFloorY() + 0.2 && !rushPlayer)
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
                    RaycastHit2D WallDetection = Physics2D.Raycast(transform.position, (transform.position + transform.forward), distance, obstacle);
                    Debug.DrawRay(transform.position, (transform.position - PlayerMovement.Instance.transform.position) * (-1));
                        if (WallDetection.collider == null)
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
                        lookRight = false;
                    }
                    else if (transform.position.x - PlayerMovement.Instance.transform.position.x > 0 && !rushPlayer && touchPlayer.collider == null)
                    {
                        Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                        transform.rotation = Quaternion.Euler(rotator);
                        lookRight = true;
                    }

                    if (touchPlayer.collider == null && canShoot)
                    {
                        canShoot = false;
                        Shoot();
                    }
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
        Instantiate(bulletRef, transform.position, Quaternion.Euler(0,0,angle));
        
        Invoke("ResetShoot",delay);
        
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
    
    private bool CheckWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1.5f, obstacle);
        if(hit.collider == null)
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(offsetX,0,0), Vector2.down, 1.5f, obstacle);
        Debug.DrawRay(transform.position + new Vector3(offsetX,0,0), Vector2.down);
        if(offsetX == 0)
        {
            floorY = hit.collider.transform.position.y;
        }
        if(hit.collider == null )
        {
            return false;
        }
        else
        {
            return true;
        }

    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == PlayerMovement.Instance.gameObject)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (PlayerMovement.Instance.gameObject != null && other.gameObject == PlayerMovement.Instance.gameObject)
        {
            isInRange = false;
        }
    }

    private void Sprinting()
    {
        if (!freezed)
        {
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
