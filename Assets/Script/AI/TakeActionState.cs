using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeActionState : State
{
    private bool canTransition;
    public TakeActionState(StateMachine machine) : base(machine)
    {

    }

    public override void OnAwake()
    {
        time = 0.0f;
        canTransition = false;
    }


    public override void Update()
    {
        time += Time.deltaTime;
        if (time > 0.8f && !canTransition)
        {
            EnemyAIManager.Instance.StartCoroutine(ChangeState());
        }
    }
     
    private IEnumerator ChangeState()
    {
        canTransition = true;
        PawnActionManager.Instance.IsTakingAction();
        stateData.selectedAction.TakeAction(stateData.targetPosition);
        stateData.selectedPawn.SpendActionPoint(stateData.selectedAction.GetActionCost());
        TurnManager.Instance.SpendTurnActionPoint(stateData.selectedAction.GetActionCost());
        PawnActionManager.Instance.SpendActionPoint();

        yield return new WaitWhile(() => PawnActionManager.Instance.GetIsTakingAction());

        canTransition = false;
        if (TurnManager.Instance.GetTurnActionPoint() > 0)
        {
            machine.Transition(StateName.Select);
            yield break;
        }
        machine.Transition(StateName.Wait);
        yield break;
    }
}
