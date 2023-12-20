using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionMenu;
    
    private PlayerInput playerinput;
    private Controles controlesScript;
    
    public Button closeOption;
    public Button optionButton;

    //private bool DoNotCall;

    private void Awake()
    {
        controlesScript = new Controles();
        playerinput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(false);
    }

    void Update()
    {
        if (controlesScript.menu.quitMenu.triggered && pauseMenu.activeSelf == true)
        {
            ClosePauseMenu();
        }

        else if (controlesScript.menu.openMenu.triggered && pauseMenu.activeSelf != true && optionMenu.activeSelf != true)
        {
            GetPauseMenu();
        }
        
        else if (controlesScript.menu.quitOption.triggered && optionMenu.activeSelf == true)
        {
            CloseOptionsPanel();
        }
    }
    
    private void OnEnable()
    {
        controlesScript.Enable();
    }

    private void OnDisable()
    {
        controlesScript.Disable();
    }

    public void ClosePauseMenu()
    {
        if (pauseMenu != null)
        {
            PlayerMovement.Instance.gameObject.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GetPauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            optionButton.Select();
            PlayerMovement.Instance.gameObject.SetActive(false);
        }
    }

    public void CallOptionsPanel()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(true);
        closeOption.Select();
    }

    public void CloseOptionsPanel()
    {
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
        optionButton.Select();
    }
}
