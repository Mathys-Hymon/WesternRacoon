using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    [SerializeField] private ButtonScript[] Buttons;
    private int IsValidInput;
    [SerializeField] private bool CloseBehind = false;
    [SerializeField] private bool CameraFocus = false;
    private float  y;
    private float x;
    private CameraMovement CameraRef;
    private PlayerMovement PlayerRef;
    private int DoorOpen = 0;
    private float delay = 4f;



    private void Start()
    {
        CameraRef = FindObjectOfType<CameraMovement>();
        y = transform.position.y;
        x = transform.position.x;
        PlayerRef = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {

      
        if (delay <= 4f)
        {
            delay += Time.deltaTime;
        }


        if(Buttons.Length > 0)       
        {
            IsValidInput = 0;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i].IsActivated()) 
            {
                IsValidInput++;
            }
            else
            {
                break;
            }
        }
        if (IsValidInput == Buttons.Length && DoorOpen == 0)
        {
            DoorOpen = 1;
                if (CameraFocus && delay >=4f)
                {
                    PlayerRef.enabled = false;
                    Invoke("OpenDoorDelay", 1f);
                    CameraRef.SetTarget(gameObject);
                    Invoke("AnimationFinished", 2f);    
                }
                else
                {
                        y = y + 2;
                }
        }
          else if (IsValidInput < Buttons.Length && DoorOpen == 1)
          {
            DoorOpen = 0;
                    y -= 2;
          }
                Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y, transform.position.z), 3 * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
        }
    }
    private void OpenDoorDelay()
    {
            y += 2;
    }

    private void AnimationFinished()
    {
        if (CameraFocus)
        {
            CameraRef.SetTarget(null);
            PlayerRef.enabled = true;
            delay = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && CloseBehind && DoorOpen < 2)
        {
            DoorOpen = 2;
            y = transform.position.y - 2; 
        }
    }
}
