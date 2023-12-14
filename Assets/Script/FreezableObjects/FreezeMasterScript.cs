using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMasterScript : MonoBehaviour
{
    protected bool freezed;
    protected float freezeTime = 3f;
    public void FreezeObject()
    {
        freezed = true;
        Invoke("ResetTimer", freezeTime);
    }
    

    private void ResetTimer()
    {
        freezed = false;
    }

    public bool isFreezed()
    {
        return freezed;
    }
}
