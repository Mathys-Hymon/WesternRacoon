using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public Button closeButton;
    public Button cancelButton;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button6))
        {
            GetPauseMenu();
            Debug.Log("Opens");
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePauseMenu);
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(Quit);
        }
    }

    void ClosePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void Quit()
    {
        Application.Quit();
    }

    void GetPauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
