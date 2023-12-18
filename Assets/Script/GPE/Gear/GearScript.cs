using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GearScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool rotateRight;

    private void Update()
    {
        if(rotateRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, -rotationSpeed * Time.timeSinceLevelLoad);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * Time.timeSinceLevelLoad);
        }
        
    }
}
