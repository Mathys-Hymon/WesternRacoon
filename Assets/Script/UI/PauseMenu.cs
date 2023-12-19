using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionMenu;
    
    public Button closeOption;
    public Button optionButton;

    private bool DoNotCall;
    void Start()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            GetPauseMenu();
            Debug.Log("Opens");
        }
    }

    public void ClosePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GetPauseMenu()
    {
        if (pauseMenu != null  && !DoNotCall)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            optionButton.Select();
        }
    }

    public void CallOptionsPanel()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(true);
        DoNotCall = true;
        closeOption.Select();
    }

    public void CloseOptionsPanel()
    {
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
        DoNotCall = false;
        optionButton.Select();
    }
}
