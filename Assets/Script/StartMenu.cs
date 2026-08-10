using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    private void Start()
    {
        SetStartButton();
        SetQuitButton();
    }
    private void SetStartButton()
    {
        startButton.onClick.AddListener(() => StartGame());
    }

    private void SetQuitButton()
    {
        quitButton.onClick.AddListener(() => QuitGame());
    }
    private void StartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
