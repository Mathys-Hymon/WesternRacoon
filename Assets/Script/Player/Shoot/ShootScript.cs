using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField] private GameObject timer;
    [SerializeField] float speed;
    [SerializeField] float freezeDuration;
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Freezeable")
            {
                if (PlayerMovement.Instance.GetFreezedObject() == null || !PlayerMovement.Instance.GetFreezedObject().Contains(collision.gameObject))
                {
                    PlayerMovement.Instance.SetFreezedObject(collision.gameObject);
                    GameObject timerRef = Instantiate(timer);
                    timerRef.transform.position = collision.gameObject.transform.position;
                    timerRef.GetComponent<CircleTimeFreezeScript>().SetTimer(freezeDuration, collision.gameObject);
                    collision.gameObject.GetComponent<FreezeMasterScript>().FreezeObject(freezeDuration);
                }
                else if (PlayerMovement.Instance.GetFreezedObject().Contains(collision.gameObject))
                {
                    CircleTimeFreezeScript[] myItems = FindObjectsOfType(typeof(CircleTimeFreezeScript)) as CircleTimeFreezeScript[];
                    for(int i = 0; i < myItems.Length; i++)
                    {
                        if(myItems[i].GetFreezedObject() == collision.gameObject)
                        {
                            Destroy(myItems[i].gameObject);
                        }
                    }
                    GameObject timerRef = Instantiate(timer);
                    timerRef.transform.position = collision.gameObject.transform.position;
                    timerRef.GetComponent<CircleTimeFreezeScript>().SetTimer(freezeDuration, collision.gameObject);
                    collision.gameObject.GetComponent<FreezeMasterScript>().FreezeObject(freezeDuration);
                }
            }
            if (collision.gameObject.layer != 6)
            {
                Destroy(gameObject);
            }
        }
    }
}
