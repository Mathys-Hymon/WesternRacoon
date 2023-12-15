using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimeFreezeScript : MonoBehaviour
{
    [SerializeField] private Sprite[] circleImage;
    private SpriteRenderer sr;
    private float time;
    private int nextImage;
    private GameObject freezedObjectRef;

    private void ChangeClock()
    {
        if (freezedObjectRef != null)
        {
            if (PlayerMovement.Instance.GetFreezedObject() != null && PlayerMovement.Instance.GetFreezedObject().Contains(freezedObjectRef))
            {
                if (nextImage < circleImage.Length)
                {
                    sr.sprite = circleImage[nextImage];
                    nextImage++;
                }
                else
                {
                    PlayerMovement.Instance.DestroyFreezedObject(freezedObjectRef);
                    Destroy(gameObject);
                }

                Invoke("ChangeClock", time / circleImage.Length);
            }
            else
            {
                PlayerMovement.Instance.DestroyFreezedObject(freezedObjectRef);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject GetFreezedObject()
    {
        return freezedObjectRef;
    }

    public void SetTimer(float newTime, GameObject ObjectRef)
    {
        freezedObjectRef = ObjectRef;
        sr = GetComponent<SpriteRenderer>();
        time = newTime;
        ChangeClock();
    }
}