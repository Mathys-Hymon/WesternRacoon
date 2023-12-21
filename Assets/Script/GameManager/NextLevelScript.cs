using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    GameObject fadeScreen, player;
    Animator animator;
    [SerializeField] int levelToLoad;
    void Awake()
    {
        fadeScreen = GameObject.Find("FadeScreen");
        player = GameObject.Find("Player");
        animator = fadeScreen.GetComponent<Animator>();
        StartCoroutine(FadeOut());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            player.GetComponent<AimingScript>().CrosshairState(false);
            animator.SetBool("FadeIn", true);
            StartCoroutine(LoadNextLevel());
        }
    }
    IEnumerator FadeOut()
    {
        player.gameObject.GetComponent<AimingScript>().CrosshairState(false);
        yield return new WaitForSeconds(1f);
        player.gameObject.GetComponent<AimingScript>().CrosshairState(true);
    }
    IEnumerator LoadNextLevel()
    {
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelToLoad);
    }
}
