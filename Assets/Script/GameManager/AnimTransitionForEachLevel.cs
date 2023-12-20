using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimTransitionForEachLevel : MonoBehaviour
{
    [SerializeField] string nextLevel;

    public Animator transition;

    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            StartCoroutine(LoadLevel(nextLevel));
        }
    }
    IEnumerator LoadLevel(string nextLevel)
    {

        transition.SetTrigger("Close");
        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevel);

        while (!operation.isDone)
        {
            yield return null;
        }
        var transitionScriptInNewScene = FindObjectOfType<AnimTransitionForEachLevel>();
        if (transitionScriptInNewScene != null)
        {
            transitionScriptInNewScene.PlayOpenAnimation();
        }
    }

    public void PlayOpenAnimation()
    {
        transition.SetTrigger("Open");
    }
}
