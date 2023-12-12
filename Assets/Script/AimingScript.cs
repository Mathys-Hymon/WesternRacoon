using UnityEngine;
using UnityEngine.InputSystem;

public class AimingScript : MonoBehaviour
{
    [SerializeField] private GameObject crosshairRef;
    [SerializeField] private LayerMask ignoreLayer;
    private bool isGamepad;
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
        }
    }
}
