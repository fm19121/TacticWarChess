using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private GridPosition winCondition = new GridPosition(15, 20);
    [SerializeField] private GameObject gameEnd;
    [SerializeField] private TextMeshProUGUI gameResult;
    [SerializeField] private Button startMenuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        Time.timeScale = 1f;
        InitMenu();
        gameEnd.SetActive(false);
        Pawn.OnPawnDestroyed += DetectLoseCondition;
        //Pawn.OnPawnDestroyed += DetectWinCondition;
    }

    private void Update()
    {
        DetectWinCondition();
    }
    private void WinMenu()
    {
        gameEnd.SetActive(true);
        gameResult.text = "!!! You WIN !!!";
        Time.timeScale = 0f;
    }

    private void LooseMenu()
    {
        gameEnd.SetActive(true);
        gameResult.text = "!!! You LOSE !!!";
        Time.timeScale = 0f;
    }

    public void DetectWinCondition()
    {
        List<Pawn> enemys = PawnManager.Instance.GetEnemyPawns();
        if( enemys.Count <= 0 ) 
        {
            WinMenu();
        }
        foreach(Pawn pawn in PawnManager.Instance.GetAllyPawns())
        {
            if(GridLevel.Instance.IsAllyInRegion(winCondition, pawn))
            {
                WinMenu();
            }
        }
    }

    public void DetectLoseCondition(object sender, EventArgs e)
    {
        List<Pawn> pawns = PawnManager.Instance.GetAllyPawns();
        if(pawns.Count <= 0) 
        {
            LooseMenu();
        }
    }

    private void InitMenu()
    {
        SetStartMenuButton();
        SetRestartButton();
        SetQuitButton();
    }
    private void SetStartMenuButton()
    {
        startMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        });
    }

    private void SetRestartButton()
    {
        restartButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
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
