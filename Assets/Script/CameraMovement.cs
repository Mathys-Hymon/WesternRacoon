using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;
    [SerializeField] private float smoothingSpeedx;
    [SerializeField] private float smoothingSpeedy;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject target;
    private float x, y, z;
    [SerializeField] private float yOffset ;
    [SerializeField] private float xOffset ;


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
        if(target = PlayerMovement.Instance.gameObject)
        {
            xOffset = (target.GetComponent<Rigidbody2D>().velocity.x / 2);
        }

        if (target.transform.position.x < minPosition.x || target.transform.position.x > maxPosition.x)
        {
            x = Mathf.Clamp(target.transform.position.x, minPosition.x, maxPosition.x);
        }
        else if (Mathf.Abs(transform.position.x - target.transform.position.x) > 0.1f)
        {
            x = target.transform.position.x + xOffset;
        }
        if (target.transform.position.y < minPosition.y || target.transform.position.y > maxPosition.y)
        {
            y = Mathf.Clamp(target.transform.position.y + yOffset, minPosition.y, maxPosition.y);
        }
        else if (Mathf.Abs(transform.position.y - target.transform.position.y) > 0.1f)
        {
            y = target.transform.position.y + yOffset;
        }
        float targetPositionX = Mathf.Lerp(transform.position.x, x, smoothingSpeedx * Time.deltaTime);
        float targetPositionY = Mathf.Lerp(transform.position.y, y, smoothingSpeedy * Time.deltaTime);
        transform.position = new Vector3(targetPositionX, targetPositionY, z);

        

    }

}