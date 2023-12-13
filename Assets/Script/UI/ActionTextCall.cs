using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTextCall : MonoBehaviour
{
    [SerializeField] GameObject canvasRef;
    void Start()
    {

    }

    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        canvasRef.SetActive(true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canvasRef.SetActive(false);
    }
}
