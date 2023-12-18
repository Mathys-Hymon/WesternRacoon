using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateformScript : FreezeMasterScript
{
    [Header("Other plateform Ref\n")]
    [SerializeField] private PressurePlateformScript otherPlateformRef;
    [SerializeField] private bool goBackInPlace;
    [SerializeField] private float maxHeight;
    [SerializeField] private float startPositionInPercent;
    [SerializeField] private float fallSpeed;
    [SerializeField] private bool startDown;

    private float initialPosition;
    private bool playerIsOn;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position + transform.up * maxHeight, gameObject.transform.localScale);
    }

    private void Awake()
    {
        if(otherPlateformRef != null)
        {
            otherPlateformRef.SetOtherPlateformRef(this);
        }
        initialPosition = transform.position.y;
    }
    public void SetOtherPlateformRef(PressurePlateformScript newRef)
    {
        otherPlateformRef = newRef;
    }

    private void Start()
    {
        if(!startDown && startPositionInPercent == 0)
        {
            transform.position = new Vector3(transform.position.x, initialPosition + maxHeight, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, initialPosition + (maxHeight*(startPositionInPercent/100)), transform.position.z);
        }

        if(otherPlateformRef != null && startDown == otherPlateformRef.GetStartDown()) 
        {
            print("Oops, it seems that there is an error, two pressure plateform are linked and in the same state that cant be acceptable, please modify it");
        }
    }
    public void SetWeight(bool detection)
    {
        playerIsOn = detection;
    }
    public bool GetWeightOnIt()
    {
        return playerIsOn;
    }

    private void Update()
    {
        if (!freezed)
        {
            if (otherPlateformRef == null)
            {
                if (playerIsOn && transform.position.y >= initialPosition)
                {
                    transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
                }
                else if (!playerIsOn && transform.position.y <= initialPosition + maxHeight)
                {
                    transform.position += new Vector3(0, fallSpeed * Time.deltaTime, 0);
                }
            }
            else if(!otherPlateformRef.freezed)
            {
                if (!otherPlateformRef.GetWeightOnIt() && playerIsOn)
                {
                    if (transform.position.y >= initialPosition)
                    {
                        transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
                    }
                }
                else if (otherPlateformRef.GetWeightOnIt() && !playerIsOn)
                {
                    if (transform.position.y <= initialPosition + maxHeight)
                    {
                        transform.position += new Vector3(0, fallSpeed * Time.deltaTime, 0);
                    }
                }
                else
                {
                    if (goBackInPlace)
                    {
                        if (startDown)
                        {
                            if (transform.position.y >= initialPosition)
                            {
                                transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
                            }
                        }
                        else
                        {
                            if (transform.position.y <= initialPosition + maxHeight)
                            {
                                transform.position += new Vector3(0, fallSpeed * Time.deltaTime, 0);
                            }
                        }
                    }
                }
            }
        }
    }

    public bool GetStartDown() { return startDown; }
}
