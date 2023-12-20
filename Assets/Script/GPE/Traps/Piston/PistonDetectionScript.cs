using UnityEngine;

public class PistonDetectionScript : MonoBehaviour
{
    [SerializeField] PistonScript scriptRef;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject || collision.gameObject.GetComponent<ShootScript>() != null)
        {
            scriptRef.PlayerIsHere(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject || collision.gameObject.GetComponent<ShootScript>() != null)
        {
            scriptRef.PlayerIsHere(false);
        }
    }
}
