using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] string nextLevel;

    public Animator transition;

    public float transitionTime = 1f;

    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTransitioning && collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            StartCoroutine(LoadLevel(nextLevel));
            DontDestroyOnLoad(gameObject);
        }
    }

    IEnumerator LoadLevel(string nextLevel)
    {
        //isTransitioning = true;

        transition.SetTrigger("Open");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevel);

        //while (!operation.isDone)
        //{
        //    yield return null;
        //}
        //isTransitioning = false;
    }
}
