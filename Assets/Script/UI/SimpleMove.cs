using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        }

    }
}
