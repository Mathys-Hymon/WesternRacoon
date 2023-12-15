using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_X : FreezeMasterScript
{
    public float speed;
    public float MaxX;
    public float MinX;
    public float waitTime;


    void Start()
    {
        transform.position = new Vector3(transform.position.y, MaxX);
        StartCoroutine(MovePiston());
    }

    IEnumerator MovePiston()
    {
        while (true)
        {
            // Move down to MinFly
            while (transform.position.x > MinX)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);

            // Move up to MaxFly
            while (transform.position.x < MaxX)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
