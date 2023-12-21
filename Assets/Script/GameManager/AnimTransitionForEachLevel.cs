using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimTransitionForEachLevel : MonoBehaviour
{
    [SerializeField] string nextLevel;

    public Animator transition;

    public float transitionTime = 1f;

    public AnimationClip closeAnimation;
    public AnimationClip openAnimation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            Invoke(nameof(LoadNextScene), transitionTime);
        }
    }

    private void LoadNextScene()
    {
        if (closeAnimation != null)
        {
            transition.Play(closeAnimation.name);
        }

        Invoke(nameof(SceneLoaded), transitionTime);
    }

    private void SceneLoaded()
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevel);
            if (operation.isDone)
            {
                if (openAnimation != null)
                {
                    transition.Play(openAnimation.name);
                }
                operation.allowSceneActivation = true;
            }
    }
}
