using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
