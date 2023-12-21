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
    
    public Button closeOption, newGameButton, continueButton, optionButton, quitButton;
    

    private bool inOption;
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
        if (inOption)
        {
            newGameButton.interactable = false;
            continueButton.interactable = false;
            optionButton.interactable = false;
            quitButton.interactable = false;
        }
        else
        {
            newGameButton.interactable = true;
            continueButton.interactable = true;
            optionButton.interactable = true;
            quitButton.interactable = true;
        }
        //joystickB --> closeOption
    }

    public void Continue()
    {
        //SaveSystem.instance.Load();
        SceneManager.LoadScene(1);
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
        inOption = true;
        optionMenu.SetActive(true);
        closeOption.Select();
    }

    public void CloseOptionsPanel()
    {
        inOption = false;
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
