using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("ClaireDebug");
    }

    public void NewGame()
    {
        SaveSystem.instance.DeleteFile();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
