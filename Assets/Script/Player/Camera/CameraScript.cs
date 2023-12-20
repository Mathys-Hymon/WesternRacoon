using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;
    [SerializeField] private float smoothingSpeed;

    [SerializeField] private GameObject PlayerRef;
    private GameObject target;
    private float x, y, z;
    private float yOffset = 3;

    public static CameraScript Instance;
    private Controles controlesScript;
    private PlayerInput playerinput;

    private void Awake()
    {
        controlesScript = new Controles();
        playerinput = GetComponent<PlayerInput>();
    }
    private void OnEnable()
    {
        controlesScript.Enable();
    }

    private void OnDisable()
    {
        controlesScript.Disable();
    }

    private void Start()
    {
        Instance = this;
        target = PlayerMovement.Instance.gameObject;
        x = transform.position.x;
        y = transform.position.y + yOffset;
        z = transform.position.z;
    }

    public void Respawn(GameObject NewTarget)
    {

        Invoke("GoToPlayer", 2f);
    }

    private void GoToPlayer()
    {

    }

    void Update()
    {
        if (target.transform.position.x < minPosition.x || target.transform.position.x > maxPosition.x)
        {
            x = Mathf.Clamp(target.transform.position.x, minPosition.x, maxPosition.x);
        }
        else if (Mathf.Abs(transform.position.x - target.transform.position.x) > 0.1f)
        {
            x = target.transform.position.x;
        }
        if (target.transform.position.y < minPosition.y || target.transform.position.y > maxPosition.y)
        {
            y = Mathf.Clamp(target.transform.position.y + yOffset, minPosition.y, maxPosition.y);
        }
        else if (Mathf.Abs(transform.position.y - target.transform.position.y) > 0.1f)
        {
            y = target.transform.position.y + yOffset;
        }
        Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(x, y, z), smoothingSpeed * Time.deltaTime);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, z);
    }
}
