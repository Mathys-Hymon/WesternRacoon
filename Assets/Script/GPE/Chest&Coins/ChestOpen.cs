using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    [Header("CoinRef")]
    [SerializeField] private GameObject coinPrefab;

    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSRC;

    private bool isOpened = false;
    private Animator animator;

    public bool GetOpen()
    {
        return isOpened;
    }
    public void SetisOpen(bool newOpen)
    {
        isOpened = newOpen;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isOpened)
        {
            audioSRC.Play();
            animator.SetBool("Opening", true);
            SpawnCoin();
            isOpened = true;
        }
    }

    private void SpawnCoin()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
