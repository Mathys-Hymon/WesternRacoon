using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSwitchScript : MonoBehaviour
{
    [SerializeField] string nextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
