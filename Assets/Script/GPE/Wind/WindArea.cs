using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    [SerializeField] private float strength;
    [SerializeField] private bool GoLeft;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            if(GoLeft)
            {
                PlayerMovement.Instance.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 0) * strength);
            }
            else
            {
                PlayerMovement.Instance.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 0) * strength);
            }
           
        }
    }
}
