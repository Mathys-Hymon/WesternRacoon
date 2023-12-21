using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] private GameObject respawnPosition;
    [SerializeField] private Light2D checkpointLight;

    private bool actualCheckpoint;

    private void Awake()
    {
        respawnPosition.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            PlayerMovement.Instance.SetNewCheckPoint(this);
            actualCheckpoint = true;
            SaveSystem.Instance.Save();
        }
    }

    private void Update()
    {
        if(actualCheckpoint)
        {

        }
    }

    public Vector2 RespawnPosition()
    {
        return respawnPosition.transform.position;
    }
}
