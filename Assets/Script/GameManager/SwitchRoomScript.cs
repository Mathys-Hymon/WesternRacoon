using UnityEngine;

public class SwitchRoomScript : MonoBehaviour
{
    [SerializeField] private int previousRoom;

    private Vector2 roomBoundary;

    private void Awake()
    {
        roomBoundary.y = transform.position.x;
        SwitchRoomScript[] otherRooms = GameObject.FindObjectsOfType<SwitchRoomScript>();
        for (int i = 0; i < otherRooms.Length; i++)
        {
            if (otherRooms[i].Room() == previousRoom + 1)
            {
                roomBoundary.x = otherRooms[i].transform.position.x;
                break;
            }
        }
    }
    public int Room()
    {
        return previousRoom;
    }
    
    public Vector2 GetBoundary()
    {
        return roomBoundary;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            CameraScript.Instance.NewCameraBoundary(roomBoundary);
        }
    }
}
