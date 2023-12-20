using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector2 boundary;

    public static CameraScript Instance;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        if(Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0)).x <= boundary.x)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Mathf.Clamp(PlayerMovement.Instance.transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0)).x, Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, Screen.height / 2, 0)).x), transform.position.y, transform.position.z), ref velocity, smoothSpeed * Time.deltaTime);
        }
        else if (Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0)).x >= boundary.y)
        {

        }

    }
    public void NewCameraBoundary(Vector2 newBoundary)
    {
        boundary = newBoundary;
    }
}
