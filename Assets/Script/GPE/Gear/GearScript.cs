using UnityEngine;

public class GearScript : FreezeMasterScript
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool rotateRight;

    private void Update()
    {
        if (!freezed)
        {
            if (rotateRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, -rotationSpeed * Time.timeSinceLevelLoad);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * Time.timeSinceLevelLoad);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!freezed)
        {
            if (collision.gameObject == PlayerMovement.Instance.gameObject)
            {
                if (rotateRight)
                {
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + (rotationSpeed / 6f) * Time.deltaTime, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                }
                else
                {
                    collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x - (rotationSpeed / 6f) * Time.deltaTime, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
                }
            }
        }
    }
}
