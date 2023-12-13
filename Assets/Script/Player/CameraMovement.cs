using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float yOffset ;
    [SerializeField] private float xOffset ;
    [SerializeField] private float smoothSpeed;

    private float x, y, z;
    private Vector3 velocity = Vector3.zero;



    void Start()
    {
        x = transform.position.x + xOffset;
        y = transform.position.y + yOffset;
        z = transform.position.z;
    }

    public void SetSmoothSpeed(float speed)
    {
       
    }

    public void SetTarget(GameObject newtarget)
    {
        if(target == null)
        {
            target = PlayerMovement.Instance.gameObject;
        }
        target = newtarget;
    }

    void Update()
    {
        y = target.transform.position.y + yOffset;
        x = target.transform.position.x + xOffset;

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(x, y, 0), ref velocity, smoothSpeed);


        //if (target = PlayerMovement.Instance.gameObject)
        //{
        //    xoffset = (target.getcomponent<rigidbody2d>().velocity.x / 2);
        //}

        //if (target.transform.position.x < minposition.x || target.transform.position.x > maxposition.x)
        //{
        //    x = mathf.clamp(target.transform.position.x, minposition.x, maxposition.x);
        //}
        //else if (mathf.abs(transform.position.x - target.transform.position.x) > 0.1f)
        //{
        //    x = target.transform.position.x + xoffset;
        //}
        //if (target.transform.position.y < minposition.y || target.transform.position.y > maxposition.y)
        //{
        //    y = mathf.clamp(target.transform.position.y + yoffset, minposition.y, maxposition.y);
        //}
        //else if (mathf.abs(transform.position.y - target.transform.position.y) > 0.1f)
        //{
        //    y = target.transform.position.y + yoffset;
        //}
        //float targetpositionx = mathf.lerp(transform.position.x, x, smoothingspeedx * time.deltatime);
        //float targetpositiony = mathf.lerp(transform.position.y, y, smoothingspeedy * time.deltatime);
        //transform.position = new vector3(targetpositionx, targetpositiony, z);



    }

}