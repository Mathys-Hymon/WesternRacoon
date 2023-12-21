using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] private GameObject respawnPosition;
    [SerializeField] private Light2D checkpointLight;

    private void Awake()
    {
        checkpointLight.intensity = 0;
        respawnPosition.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            PlayerMovement.Instance.SetNewCheckPoint(this);
            SaveSystem.Instance.Save();
            checkpointLight.intensity = 3f;
        }
    }

    public Vector2 RespawnPosition()
    {
        return respawnPosition.transform.position;
    }
}
