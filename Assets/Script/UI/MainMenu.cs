using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
    
    public Button closeOption;
    public Button newGameButton;
    public Button continueButton;

    private void Start()
    {
        newGameButton.Select();
        optionMenu.SetActive(false);
        
        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            continueButton.interactable = false;
        }
    }

    private void Update()
    {
        //joystickB --> closeOption
    }

    public void Continue()
    {
        SceneManager.LoadScene("ClaireDebug");
    }

    public void NewGame()
    {
        DeleteFile();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
    public void CallOptionsPanel()
    {
        optionMenu.SetActive(true);
        closeOption.Select();
    }

    public void CloseOptionsPanel()
    {
        optionMenu.SetActive(false);
        newGameButton.Select();
    }
    
    public void DeleteFile()
    {
        string json = Application.persistentDataPath + "/data.save";
    
        // check if file exists
        if (!File.Exists(json))
        {
            SceneManager.LoadScene("ClaireDebug");
        }
        else
        {
            File.Delete(json);
            SceneManager.LoadScene("ClaireDebug");
        }
    }
}
