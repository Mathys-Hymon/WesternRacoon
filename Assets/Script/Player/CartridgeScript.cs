using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeScript : MonoBehaviour
{
    private void Start()
    {
        Invoke("Destroy", 3f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
