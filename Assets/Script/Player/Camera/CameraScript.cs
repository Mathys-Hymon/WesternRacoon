using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float speed;

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
    }

    private void Update()
    {
        transform.position = PlayerMovement.Instance.gameObject.transform.position + new Vector3(controlesScript.player.move.ReadValue<float>(),0); ;
    }
}
