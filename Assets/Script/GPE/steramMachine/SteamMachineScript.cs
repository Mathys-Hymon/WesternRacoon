using UnityEngine;

public class SteamMachineScript : MonoBehaviour
{
    [Header("ObjectReferences")]
    [SerializeField] private ButtonScript[] buttons;
    [SerializeField] private ParticleSystem steamParticle;
    [SerializeField] private LayerMask obstacle;
    [Header("Only for automatic Steam")]
    [SerializeField] private float pauseTime;
    [SerializeField] private float steamTime;

    private int IsValidInput;
    private bool pushCreate;

    private void Start()
    {
        if(buttons.Length == 0)
        {
            pushCreate = true;
            Invoke("AutomaticSteam", steamTime);
        }
    }

    private void AutomaticSteam()
    {
        if(pushCreate)
        {
            pushCreate = false;
            Invoke("AutomaticSteam", pauseTime);
        }
        else
        {
            pushCreate = true;
            Invoke("AutomaticSteam", steamTime);
        }
    }

    private void Update()
    {
        if (pushCreate)
        {
            steamParticle.Play();
        }
        else if (!pushCreate)
        {
            steamParticle.Stop();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (buttons.Length > 0)
        {
            IsValidInput = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].IsActivated())
                {
                    IsValidInput++;
                }
                else
                {
                    break;
                }
            }

            if (IsValidInput == buttons.Length && collision.gameObject.GetComponent<BoxScript>() != null && collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                float distance = Vector3.Distance(transform.position, collision.gameObject.transform.position);
                RaycastHit2D touchPlayer = Physics2D.Raycast(transform.position, (transform.position - collision.gameObject.transform.position) * (-1), distance, obstacle);
                Debug.DrawRay(transform.position, (transform.position - collision.gameObject.transform.position) * (-1));

                if (touchPlayer.collider == null || touchPlayer.collider.gameObject.GetComponent<BoxScript>())
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = (transform.up * 10) / Vector3.Distance(transform.position, collision.transform.position);
                }
             }
        }
        else
        {
            if (pushCreate && collision.gameObject.GetComponent<BoxScript>() != null && collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                float distance = Vector3.Distance(transform.position, collision.gameObject.transform.position);
                RaycastHit2D touchPlayer = Physics2D.Raycast(transform.position + transform.up, (transform.position - collision.gameObject.transform.position) * (-1), distance, obstacle);
                Debug.DrawRay(transform.position + transform.up, (transform.position - collision.gameObject.transform.position) * (-1));

                if (touchPlayer.collider == null || touchPlayer.collider.gameObject.GetComponent<BoxScript>() != null)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = (transform.up * 10) / Vector3.Distance(transform.position, collision.transform.position);
                }
            }
        }
        if (collision.gameObject == PlayerMovement.Instance.gameObject && pushCreate)
        {
            float distance = Vector3.Distance(transform.position, collision.gameObject.transform.position);
            RaycastHit2D touchPlayer = Physics2D.Raycast(transform.position, (transform.position - collision.gameObject.transform.position) * (-1), distance, obstacle);
            Debug.DrawRay(transform.position, (transform.position - collision.gameObject.transform.position) * (-1));

            if (touchPlayer.collider == null)
            {
                PlayerMovement.Instance.Die();
            }
        }
    }
}
