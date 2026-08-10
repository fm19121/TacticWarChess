using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public event Action OnPlayerTurnEnd;
    public event Action ToNextTurn;

    private int actionPoint = 3;
    private int turnNum = 1;
    private bool isPlayerTurn = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        ToNextTurn += ResetTurnActionPoint;
        OnPlayerTurnEnd += ResetTurnActionPoint;
    }

    public void EndPlayerTurn()
    {
        //isPlayerTurn = !isPlayerTurn;
        isPlayerTurn = false;
        OnPlayerTurnEnd?.Invoke();
    }
    public void NextTurn()
    {
        turnNum++;
        //isPlayerTurn = !isPlayerTurn;
        Debug.Log("end Turn");
        isPlayerTurn = true;
        ToNextTurn?.Invoke();
    }

    public int GetTurnNumber()
    {
        return turnNum;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }

    public int GetTurnActionPoint()
    {
        return actionPoint;
    }

    public void SpendTurnActionPoint(int cost)
    {
        actionPoint -= cost;
    }


    private void ResetTurnActionPoint()
    {
        actionPoint = 3;
    }
}
