using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMasterScript : MonoBehaviour
{
    [SerializeField] protected bool freezed;
    [SerializeField] protected float freezeTime;

    private float timer;

    public void FreezeObject()
    {
        freezed = true;
    }

    private void Update()
    {
        if (freezed && timer <= freezeTime) 
        {
            timer += Time.deltaTime;
        }
        
        else if (freezed && timer >= freezeTime)
        {
            freezed = false;
            timer = 0;
        }
    }
}
