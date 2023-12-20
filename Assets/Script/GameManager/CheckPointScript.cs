using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] private GameObject respawnPosition;

    private void Awake()
    {
        respawnPosition.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            PlayerMovement.Instance.SetNewCheckPoint(this);
        }
    }

    public Vector2 RespawnPosition()
    {
        return respawnPosition.transform.position;
    }
}
