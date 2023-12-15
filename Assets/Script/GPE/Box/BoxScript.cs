using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxScript : FreezeMasterScript
{
    private Rigidbody2D rb2d;
    private bool flipflop;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(freezed && flipflop)
        {
            flipflop = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (!freezed && !flipflop)
        {
            flipflop= true;
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }


}
