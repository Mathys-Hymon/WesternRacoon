using UnityEngine;
using UnityEngine.InputSystem;

public class AimingScript : MonoBehaviour
{
    [SerializeField] private GameObject crosshairRef;
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private GameObject cartridgeRef;
    private bool isGamepad;
    private Controles controlesScript;
    private PlayerInput playerinput;

    private void Awake()
    {
        controlesScript = new Controles();
        playerinput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
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

            if(Mathf.Abs(mouseMultiplyer.x) + Mathf.Abs(mouseMultiplyer.y) <= 0.2f)
            {
                crosshairRef.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                crosshairRef.GetComponent<SpriteRenderer>().enabled = true;
                crosshairRef.transform.position = new Vector3(transform.position.x + 4 * mouseMultiplyer.x, transform.position.y + 4 * mouseMultiplyer.y, 0);
            }

            if (controlesScript.player.shoot.triggered)
            {
                GameObject emptyMun = Instantiate(cartridgeRef, transform.position, Quaternion.identity);
                emptyMun.GetComponent<Rigidbody2D>().velocity = new Vector3(-3 * mouseMultiplyer.x, 4, 0);
                print(Mathf.Clamp(Camera.main.ScreenToWorldPoint(controlesScript.player.aim.ReadValue<Vector2>()).x, -5, 5));
                emptyMun.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 350));
            }
        }
        else
        {
            Cursor.visible = false;
            crosshairRef.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(controlesScript.player.aim.ReadValue<Vector2>());
            mousePos.z = 0;
            crosshairRef.transform.position = mousePos;

            if (controlesScript.player.shoot.triggered)
            {
                GameObject emptyMun = Instantiate(cartridgeRef, transform.position, Quaternion.identity);
                emptyMun.GetComponent<Rigidbody2D>().velocity = new Vector3(3 * Mathf.Clamp(transform.position.x - Camera.main.ScreenToWorldPoint(controlesScript.player.aim.ReadValue<Vector2>()).x, -1, 1), 4, 0);
                emptyMun.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 350));
            }
        }
    }
}
