using UnityEngine;
using UnityEngine.InputSystem;

public class AimingScript : MonoBehaviour
{
    [Header("REFERENCES\n")]
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private GameObject cartridgeRef;
    private bool isGamepad;
    private Controles controlesScript;
    private PlayerInput playerinput;
    private GameObject crosshairRef;
    private GameObject ArmTarget;
    private bool enableCrosshair = true;

    private void Awake()
    {
        controlesScript = new Controles();
        playerinput = GetComponent<PlayerInput>();
        ArmTarget = GameObject.Find("ArmTargetPosition");
        crosshairRef = GameObject.Find("Crosshair");
    }
    private void OnEnable()
    {
        controlesScript.Enable();
    }
    private void OnDisable()
    {
        controlesScript.Disable();
    }
    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad")? true : false;
    }
    private void Update()
    {
        if(isGamepad)
        {
            Vector2 mouseMultiplyer = controlesScript.player.aim.ReadValue<Vector2>();
            if (Mathf.Abs(mouseMultiplyer.x) + Mathf.Abs(mouseMultiplyer.y) <= 0.2f || !enableCrosshair)
            {
                crosshairRef.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                crosshairRef.GetComponent<SpriteRenderer>().enabled = true;
            }

                crosshairRef.transform.position = new Vector3(transform.position.x + (4 * mouseMultiplyer.x), transform.position.y + (4 * mouseMultiplyer.y), 0);
                ArmTarget.transform.position = new Vector3(transform.position.x+mouseMultiplyer.x, transform.position.y + mouseMultiplyer.y, 0);

            if (controlesScript.player.shoot.triggered)
            {
                if(crosshairRef.GetComponent<SpriteRenderer>().enabled)
                {
                    Vector2 DirectiontoTarget = crosshairRef.transform.position - transform.position;
                    float angle = -90 + Mathf.Atan2(DirectiontoTarget.y, DirectiontoTarget.x) * Mathf.Rad2Deg;
                    Instantiate(bulletRef, transform.position, Quaternion.Euler(0, 0, angle));

                }
                else
                {
                    Vector2 DirectiontoTarget = PlayerMovement.Instance.transform.right;
                    float angle = -Mathf.Atan2(DirectiontoTarget.x, DirectiontoTarget.y)*Mathf.Rad2Deg;

                    Instantiate(bulletRef, transform.position, Quaternion.Euler(0, 0, angle));
                }
                //GameObject emptyMun = Instantiate(cartridgeRef, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 350)));
                //emptyMun.GetComponent<Rigidbody2D>().velocity = new Vector3(-3 * mouseMultiplyer.x, 4, 0);
            }
        }
        else
        {
            if (!enableCrosshair)
            {
                crosshairRef.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                Cursor.visible = false;
                crosshairRef.GetComponent<SpriteRenderer>().enabled = true;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(controlesScript.player.aim.ReadValue<Vector2>());
                mousePos.z = 0;
                crosshairRef.transform.position = mousePos;
                ArmTarget.transform.position = mousePos;
            }
           
            if (controlesScript.player.shoot.triggered)
            {
                Vector2 DirectiontoTarget = crosshairRef.transform.position - transform.position;
                float angle = -90 + Mathf.Atan2(DirectiontoTarget.y, DirectiontoTarget.x) * Mathf.Rad2Deg;
                Instantiate(bulletRef, transform.position, Quaternion.Euler(0, 0, angle));

                //GameObject emptyMun = Instantiate(cartridgeRef, transform.position, Quaternion.identity);
                //emptyMun.GetComponent<Rigidbody2D>().velocity = new Vector3(3 * Mathf.Clamp(transform.position.x - Camera.main.ScreenToWorldPoint(controlesScript.player.aim.ReadValue<Vector2>()).x, -1, 1), 4, 0);
                //emptyMun.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 350));
            }
        }
    }

    public bool CrosshairState(bool crosshairState)
    {
        return enableCrosshair = crosshairState;
    }
}
