using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : FreezeMasterScript
{

    [Header("ObjectReferences")]
    [SerializeField] private ButtonScript[] buttons;
    [SerializeField] private CogScript cogRef;

    [Header("openingInfos")]
    [SerializeField] private float openingSize;
    [SerializeField] private float openingTime;
    [SerializeField] private bool closeBehind = false;


    private float  y;
    private float initialPosition;
    private int doorOpen = 0;
    private int IsValidInput;

    private void Start()
    {
        y = transform.position.y;
        initialPosition = y;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position+ transform.right*openingSize, new Vector3(1,1, 0));
    }

    void Update()
    {
      if (!freezed && !cogRef.isFreezed()) {
        if(buttons.Length > 0)       
        {
            IsValidInput = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].IsActivated()) 
            {
                IsValidInput++;
            }
            else
            {
                break;
            }
        }
        if (IsValidInput == buttons.Length && doorOpen == 0)
        {
            doorOpen = 1;
            y = initialPosition + openingSize;
        }
          else if (IsValidInput < buttons.Length && doorOpen == 1)
          {
            doorOpen = 0;
                    y = initialPosition;
          }
                Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y, transform.position.z), openingTime * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
                cogRef.transform.rotation = Quaternion.Euler(0,0,targetPosition.y*80);
        }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && closeBehind && doorOpen < 2)
        {
            doorOpen = 2;
            y = initialPosition; 
        }
    }
}
