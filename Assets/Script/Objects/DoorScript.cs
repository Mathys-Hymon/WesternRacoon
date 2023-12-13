using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : FreezeMasterScript
{

    [SerializeField] private ButtonScript[] Buttons;
    [SerializeField] private bool CloseBehind = false;
    private int IsValidInput;
    private float  y;
    private int DoorOpen = 0;



    private void Start()
    {
        y = transform.position.y;
    }

    void Update()
    {

      if(!freezed) {
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
            y = y + 2;
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
