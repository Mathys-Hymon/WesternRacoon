using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPDetectionScript : MonoBehaviour
{
    [SerializeField] PressurePlateformScript plateformRef;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxScript>() != null)
        {
            plateformRef.SetWeight(2);
        }
        else if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            plateformRef.SetWeight(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject || collision.gameObject.GetComponent<BoxScript>() != null)
        {
            plateformRef.SetWeight(0);
        }
    }
}
