using UnityEngine;

public class MovingPlateformScript : FreezeMasterScript
{
    [Header("ObjectReferences\n")]
    [SerializeField] private ButtonScript[] buttons;
    [SerializeField] private CogScript cogRef;

    [Header("MOVEMENT INFOS\n")]
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;

    [Header("\n\nWORLD DIRECTION AND POSITION")]
    [Header("put the X value if you check isHorizontal, else put the Y value\n")]
    [SerializeField] private bool isHorizontal;
    [SerializeField] private float targetPosition;

    private float initialPositionY;
    private float y;
    private bool goPointB;
    private GameObject target = null;
    private Vector3 offset;
    private Vector3 cogOffset;
    private int IsValidInput;


    void Start()
    {
        target = null;
        if(cogRef != null)
        {
            cogOffset = cogRef.transform.position - transform.position;
        }
        if (!isHorizontal)
        {
            y = transform.position.y;
        }
        else
        {
            y = transform.position.x;
        }
        initialPositionY = y;

        if(buttons.Length == 0)
        {
            Invoke("FlipFlopPosition", waitTime);
        }
    }

    private void FlipFlopPosition()
    {
        if (!goPointB)
        {
            goPointB = true;
            y = targetPosition;
        }
        else
        {
            goPointB = false;
            y = initialPositionY;
        }
        Invoke("FlipFlopPosition", waitTime);
    }

    private void Update()
    {
        if (cogRef != null)
        {
            if (!freezed && !cogRef.isFreezed())
            {
                UpdatePlateformPosition();
            }
        }
        else
        {
            if (!freezed)
            {
                UpdatePlateformPosition();
            }
        }
    }

    private void UpdatePlateformPosition()
    {
        if (buttons.Length == 0)
        {
            if (!isHorizontal)
            {
                if(transform.position.y == y)
                {
                    
                }
                else if ((transform.position.y - y) < -0.3f)
                {
                    transform.position += new Vector3(0, (speed / 5) * Time.deltaTime, 0);
                }
                else if ((transform.position.y - y) > 0.3f)
                {
                    transform.position -= new Vector3(0, (speed / 5) * Time.deltaTime, 0);
                }
                if (cogRef != null)
                {
                    cogRef.transform.rotation = Quaternion.Euler(0, 0, transform.position.y * 80);
                }
            }
            else
            {
                if ((transform.position.x - y) < -0.01f)
                {
                    transform.position += new Vector3((speed / 5) * Time.deltaTime, 0, 0);
                }
                else if ((transform.position.x - y) > 0.01f)
                {
                    transform.position -= new Vector3((speed / 5) * Time.deltaTime, 0, 0);
                }
                if (cogRef != null)
                {
                    cogRef.transform.position = transform.position + cogOffset;
                    cogRef.transform.rotation = Quaternion.Euler(0, 0, transform.position.x * 80);
                }
            }
        }
        else
        {
            IsValidInput = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].IsActivated())
                {
                    IsValidInput++;
                }
                else
                {
                    break;
                }
            }
            if (IsValidInput == buttons.Length)
            {
                y = targetPosition;
            }
            else if (IsValidInput < buttons.Length)
            {
                y = initialPositionY;
            }

            if (!isHorizontal)
            {
                if ((transform.position.y - y) < -0.1f)
                {
                    transform.position += new Vector3(0, (speed / 5) * Time.deltaTime, 0);
                }
                else if ((transform.position.y - y) > 0.1f)
                {
                    transform.position -= new Vector3(0, (speed / 5) * Time.deltaTime, 0);
                }
                if (cogRef != null)
                {
                    cogRef.transform.rotation = Quaternion.Euler(0, 0, transform.position.y * 80);
                }
            }
            else
            {
                if ((transform.position.x - y) < -0.01f)
                {
                    transform.position += new Vector3((speed / 5) * Time.deltaTime, 0, 0);
                }
                else if ((transform.position.x - y) > 0.01f)
                {
                    transform.position -= new Vector3((speed / 5) * Time.deltaTime, 0, 0);
                }
                if (cogRef != null)
                {
                    cogRef.transform.position = transform.position + cogOffset;
                    cogRef.transform.rotation = Quaternion.Euler(0, 0, transform.position.x * 80);
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            target = collision.gameObject;
            offset = target.transform.position - transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if(target.GetComponent<FreezeMasterScript>() != null)
            {
                if(!target.GetComponent<FreezeMasterScript>().isFreezed())
                {
                    target.transform.position = transform.position + offset;
                }
            }
            else
            {
                target.transform.position = transform.position + offset;
            }
        }

    }
}
