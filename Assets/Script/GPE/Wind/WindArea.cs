using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{
    [SerializeField] private float strength;
    [SerializeField] private float XDirection;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            PlayerMovement.Instance.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(XDirection, 0) *  strength);
        }
    }
}
