using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    private bool displayMenu = false;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    void Start()
    {
        SetRestartButton();
        SetContinueButton();
        SetQuitButton();
        menu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            displayMenu = !displayMenu;
            menu.SetActive(displayMenu);
            Time.timeScale = displayMenu ? 0f : 1f;
        }
    }

    private void SetContinueButton()
    {
        continueButton.onClick.AddListener(() => 
        { 
            displayMenu = false;
            menu.SetActive(false);
            Time.timeScale = 1f;
        });
    }
    private void SetRestartButton()
    {
        restartButton.onClick.AddListener(() => 
        { 
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
        });
    }

    private void SetQuitButton()
    {
        quitButton.onClick.AddListener(() => 
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        });
    }
}
