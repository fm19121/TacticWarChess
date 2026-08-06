using System.Collections.Generic;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{
    private StateMachine stateMachine;
    private StateData stateData;

    public static EnemyAIManager Instance { get; private set; }

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
        stateData = new StateData();
        //currentState = new EnemyState(() => { }, WaitToPawnSelection);
        stateMachine = new StateMachine(stateData);
        State waitState = new Wait(stateMachine);
        State selectionState = new PawnSelectionState(stateMachine);
        State takeAction = new TakeActionState(stateMachine);
        stateMachine.currentState = waitState;
        stateMachine.AddState(StateName.Wait, waitState);
        stateMachine.AddState(StateName.Select, selectionState);
        stateMachine.AddState(StateName.TakeAction, takeAction);
    }

    // Update is called once per frame
    void Update()
    {
        if (!TurnManager.Instance.IsPlayerTurn())
        {
            stateMachine.Update();
        }
    }
    
}
