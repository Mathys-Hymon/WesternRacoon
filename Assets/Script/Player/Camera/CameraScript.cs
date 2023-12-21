using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float smoothSpeed;
    [SerializeField] private int startRoom;


    public static CameraScript Instance;
    private Vector3 velocity = Vector3.zero;
    private Vector2 offset;
    private Vector2 boundary;


    private void Start()
    {
        Instance = this;
        offset.x = transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0)).x;
        offset.y = transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0)).x;

        if(startRoom != 0)
        {
            SwitchRoomScript[] otherRooms = GameObject.FindObjectsOfType<SwitchRoomScript>();
            for (int i = 0; i < otherRooms.Length; i++)
            {
                if (otherRooms[i].Room() == startRoom)
                {
                    print("newBoundary");
                    boundary = otherRooms[i].GetBoundary();
                    break;
                }
            }
        }
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

    public void Shake(float strenght, float duration)
    {
        StartCoroutine(CoroutineShake(strenght/20f, duration));
    }

    public IEnumerator CoroutineShake(float strenght, float duration)
    {
        print("cameraShake");
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * strenght;
            float y = Random.Range(-1f, 1f) * strenght;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
    public void NewCameraBoundary(Vector2 newBoundary)
    {
        boundary = newBoundary;
    }
}
