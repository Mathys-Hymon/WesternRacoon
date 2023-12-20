using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private List<Image> panels = new List<Image>();
    [SerializeField] private float delay;
    [SerializeField] private int sceneToLoad;
    int image = 0;
    void Start()
    {
       
        for (int i = 0; i < sprites.Count; i++)
        {
            panels[i].sprite = sprites[i];
            panels[i].preserveAspect = true;
        }
        Invoke("FadeDisplay",delay);
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ImmediateDisplay();
        }

        if (image >= sprites.Count)
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    private void FadeDisplay()
    {
       
        if (image < sprites.Count)
        {
            panels[image].GetComponent<Animator>().SetBool("FadeIn", true);
            image++;
        }
        Invoke("FadeDisplay", delay);
    }

    private void ImmediateDisplay()
    {
        panels[image].GetComponent<Animator>().SetBool("Display", true);
        image++;

        Invoke("FadeDisplay", delay);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
