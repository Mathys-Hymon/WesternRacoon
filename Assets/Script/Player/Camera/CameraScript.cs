using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector2 boundary;


    public static CameraScript Instance;
    private Vector3 velocity = Vector3.zero;
    private Vector2 offset;


    private void Start()
    {
        Instance = this;
        offset.x = transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0)).x;
        offset.y = transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0)).x;
    }
    private void Update()
    {
        Vector3 targetPosition;

        if ((boundary.x - boundary.y) < offset.y*2)
        {
            float center = (boundary.x + boundary.y) / 2;
            targetPosition = new Vector3(center, transform.position.y, transform.position.z);
        }
        else
        {
            float clampedX = Mathf.Clamp(PlayerMovement.Instance.transform.position.x, boundary.y + offset.y, boundary.x + offset.x);
            targetPosition = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed*Time.deltaTime);
    }

    public void NewCameraBoundary(Vector2 newBoundary)
    {
        boundary = newBoundary;
    }
}
