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
                GameObject timerRef = Instantiate(timer, collision.gameObject.transform);
                timerRef.GetComponent<CircleTimeFreezeScript>().SetTimer(freezeDuration);
                
                collision.gameObject.GetComponent<FreezeMasterScript>().FreezeObject(freezeDuration);
            }
            if (collision.gameObject.layer != 6)
            {
                Destroy(gameObject);
            }
        }
    }
}
