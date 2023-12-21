using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenAppear : MonoBehaviour
{
    public Canvas loadingCanvas;
    private bool hasClosedAnimationPlayed = true;

    private void Start()
    {
        loadingCanvas.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator animator = GetComponent<Animator>();

        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            if (animator != null && !hasClosedAnimationPlayed)
            {
                loadingCanvas.enabled = true;
                animator.SetTrigger("CloseCircleAnim");
                hasClosedAnimationPlayed = true;
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Animator animator = GetComponent<Animator>();
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (hasClosedAnimationPlayed)
        {
            animator.SetTrigger("OpenCircleAnim");
            if (loadingCanvas != null)
            {
                loadingCanvas.enabled = false; // Disable the canvas
            }
        }
    }
}
