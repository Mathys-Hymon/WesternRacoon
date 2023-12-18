using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateformScript : MonoBehaviour
{
    [Header("\n\nWORLD DIRECTION AND POSITION")]
    [Header("put the X value if you check isHorizontal, else put the Y value\n")]
    [SerializeField] private bool isHorizontal;
    [SerializeField] private float targetDownPosition;
}
