using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] string nextLevel;

    public Canvas loadingCanvas;
    public Image circle;
    Animator animator;


    private void OnEnable()
    {
        Time.timeScale = 1.0f;
        loadingCanvas.enabled = true;
        animator = GetComponentInChildren<Animator>();
        DontDestroyOnLoad(gameObject);
        StartCoroutine("AnimateOpen", 1f);
    }

    IEnumerator AnimateOpen()
    {
        circle.gameObject.SetActive(true);
        GetComponentInChildren<Mask>().showMaskGraphic = true;
        yield return null;
        GetComponentInChildren<Mask>().showMaskGraphic = false;
        animator.SetTrigger("Open");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            animator.SetTrigger("Close");
            StartCoroutine(LoadSceneAsync());
        }
    }
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevel);
        loadingCanvas.enabled = true;
        Time.timeScale = 0.0f;
        //animator.SetTrigger("Close");
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            yield return null;
        }
        animator.SetTrigger("Close");
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
    }
}
