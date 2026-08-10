using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PawnSelectionState : State
{
    private const float SELECT_TIME = 1.0f;
    public PawnSelectionState(StateMachine machine) : base(machine)
    {

    }

    public override void OnAwake()
    {
        time = 0.0f;
    }


    public override void Update()
    {
        time += Time.deltaTime;
        if (time > SELECT_TIME)
        {
            if (TurnManager.Instance.GetTurnActionPoint() < 1 || !HasPawnActionPoint())
            {
                TurnManager.Instance.NextTurn();
                Debug.Log("To wait");
                machine.Transition(StateName.Wait);
                time = 0.0f;
                return;
            }
            if (HasTargetInAttackRange())
            {
                stateData.selectedAction = stateData.selectedPawn.GetPawnAttack();
                machine.Transition(StateName.TakeAction);
                time = 0.0f;
                return;
            }
            else if (BuffLightArcher())
            {
                machine.Transition(StateName.TakeAction);
                time = 0.0f;
                return;
            }
            else if (TakeLightCasterSkill())
            {
                machine.Transition(StateName.TakeAction);
                time = 0.0f;
                return;
            }
            else if(IsAnyEnemyCanMove())
            {
                machine.Transition(StateName.TakeAction);
                time = 0.0f;
                return;
            }
            else
            {
                TurnManager.Instance.NextTurn();
                machine.Transition(StateName.Wait);
                time = 0.0f;
                return;
            }
        }
    }

    private bool IsAnyEnemyCanMove()
    {
        List<Pawn> pawns = GetSuitablePawns();
        if(pawns.Count < 1)
        {
            pawns = PawnManager.Instance.GetEnemyPawns();
        }
        if(pawns.Count > 0) 
        {
            Pawn soldier = pawns.Find(p => p is LightSoldier);
            Pawn rider = pawns.Find(p => p is DarkRider);
            if (soldier != null && soldier.GetActionPoint() > 0)
            {
                GetMovePosition(soldier);
                stateData.selectedPawn = soldier;
                stateData.selectedAction = soldier.GetComponent<PawnMovement>();
                return true;
            }
            else if(rider != null && rider.GetActionPoint() > 0)
            {
                GetMovePosition(rider);
                stateData.selectedPawn = rider;
                stateData.selectedAction = rider.GetComponent<PawnMovement>();
                return true;
            }
            else
            {
                foreach(Pawn pawn in pawns)
                {
                    if(pawn is GuardTower)
                    {
                        continue;
                    }
                    if (pawn.GetActionPoint() > 0)
                    {
                        GetMovePosition(pawn);
                        stateData.selectedPawn = pawn;
                        stateData.selectedAction = pawn.GetComponent<PawnMovement>();
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void GetMovePosition(Pawn pawn)
    {
        GridPosition target = GetMixDistanceAlly(pawn, PawnManager.Instance.GetAllyPawns());
        GridPosition moveDir = target - pawn.GetGridPosition();
        float squareX = moveDir.x * moveDir.x;
        float squareZ = moveDir.z * moveDir.z;
        float distance = MathF.Sqrt((squareX + squareZ));
        int x = (int)(moveDir.x / distance);
        int z = (int)(moveDir.z / distance);
        if (distance <= pawn.GetMoveRange())
        {
            GridPosition offset = new GridPosition(x, z);
            stateData.targetPosition = pawn.GetGridPosition() - offset;
        }
        else
        {
            x = x * pawn.GetMoveRange();
            z = z * pawn.GetMoveRange();
            GridPosition offset = new GridPosition(x, z);
            stateData.targetPosition = offset + pawn.GetGridPosition();
        }
    }
    private bool HasTargetInAttackRange()
    {
        List<Pawn> pawns = PawnManager.Instance.GetEnemyPawns();
        if (pawns.Count <= 0)
        {
            return false;
        }
        foreach (Pawn pawn in pawns)
        {
            Pawn targetPawn = GetAttackablePawn(pawn);
            if (targetPawn != null)
            {
                if(pawn.GetActionPoint() > 0)
                {
                    stateData.selectedPawn = pawn;
                    stateData.targetPosition = targetPawn.GetGridPosition();
                    return true;
                }
            }
        }
        return false;
    }

    private bool HasPawnActionPoint()
    {
        List<Pawn> enemys = PawnManager.Instance.GetEnemyPawns();
        int actionPointSum = 0;
        if(enemys.Count <= 0)
        {
            return false;
        }
        foreach (Pawn pawn in enemys)
        {
            actionPointSum += pawn.GetActionPoint();
        }
        if(actionPointSum > 0)
        {
            return true; 
        }
        return false;
    }

    private bool TakeLightCasterSkill()
    {
        List<Pawn> enemyPawns = PawnManager.Instance.GetEnemyPawns();
        Pawn lightCaster= enemyPawns.Find(p => p is LightCaster);
        if (lightCaster)
        {
            BaseAction enhancement = lightCaster.GetComponent<Enhancement>();
            foreach(Pawn enemy in enemyPawns)
            {
                if (enhancement.IsInActionRegion(enemy.GetGridPosition()) && !enemy.GetPawnBuffs().Contains(new BuffObject(BuffObject.BuffName.LightEnhancement)))
                {
                    int actionPoint = lightCaster.GetActionPoint();
                    int turnActionPoint = TurnManager.Instance.GetTurnActionPoint();
                    if ((actionPoint >= enhancement.GetActionCost()) && turnActionPoint >= enhancement.GetActionCost())
                    {
                        stateData.selectedPawn = lightCaster;
                        stateData.targetPosition = enemy.GetGridPosition();
                        stateData.selectedAction = enhancement;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool BuffLightArcher()
    {
        List<Pawn> enemyPawns = PawnManager.Instance.GetEnemyPawns();
        Pawn lightArcher = enemyPawns.Find(p => p is LightArcher);
        if (lightArcher)
        {
            if (!lightArcher.GetPawnBuffs().Contains(new BuffObject(BuffObject.BuffName.LightArrow)))
            {
                int actionPoint = lightArcher.GetActionPoint();
                int turnActionPoint = TurnManager.Instance.GetTurnActionPoint();
                BaseAction drawBow = lightArcher.GetComponent<DrawBow>();
                if ((actionPoint >= drawBow.GetActionCost()) && turnActionPoint >= drawBow.GetActionCost())
                {
                    stateData.selectedPawn = lightArcher;
                    stateData.targetPosition = lightArcher.GetGridPosition();
                    stateData.selectedAction = drawBow;
                    return true;
                }
            }
        }
        return false;
    }

    private Pawn GetAttackablePawn(Pawn pawn)
    {
        List<Pawn> allys = PawnManager.Instance.GetAllyPawns();
        foreach (Pawn allyPawn in allys)
        {
            BaseAction attack = pawn.GetPawnAttack();
            if (attack.IsInActionRegion(allyPawn.GetGridPosition()))
            {
                return allyPawn;
            }
        }
        return null;
    }

    private List<Pawn> GetSuitablePawns()
    {
        ElementType environment = EnvironmentSystem.Instance.GetCurrentEnvironmentElement();
        List<Pawn> enemys = PawnManager.Instance.GetEnemyPawns();
        List<Pawn> suitableEnemys = new List<Pawn>();
        if (environment == ElementType.Light)
        {
            foreach (Pawn pawn in enemys)
            {
                if (IsLightPawnInLigh(pawn) || IsDarkInLight(pawn))
                {
                    suitableEnemys.Add(pawn);
                }
            }
        }
        else
        {
            foreach (Pawn pawn in enemys)
            {
                if (IsDarkPawnInDark(pawn) || IsLightInDark(pawn))
                {
                    suitableEnemys.Add(pawn);
                }
            }
        }
        return suitableEnemys;
    }

    private GridPosition GetMixDistanceAlly(Pawn enemy, List<Pawn> allys)
    {
        int distance = 10000;
        GridPosition target = new GridPosition();
        GridPosition enemyPos = enemy.GetGridPosition();
        foreach(Pawn pawn in allys)
        {
            GridPosition pawnPos = pawn.GetGridPosition();
            int temp = Math.Abs(enemyPos.x - pawnPos.x) + Math.Abs(enemyPos.z - pawnPos.z);
            if(distance > temp)
            {
                distance = temp;
                target = pawnPos;
            }
        }

        return target;
    }
    private bool IsLightPawnInLigh(Pawn pawn)
    {
        return pawn.GetElementType() == ElementType.Light && pawn.GetAffectedElement() != ElementType.Dark;
    }

    private bool IsDarkPawnInDark(Pawn pawn)
    {
        return pawn.GetElementType() == ElementType.Dark && pawn.GetAffectedElement() != ElementType.Light;
    }

    private bool IsLightInDark(Pawn pawn)
    {
        return pawn.GetElementType() == ElementType.Light && pawn.GetAffectedElement() == ElementType.Light;
    }

    private bool IsDarkInLight(Pawn pawn)
    {
        return pawn.GetElementType() == ElementType.Dark && pawn.GetAffectedElement() == ElementType.Dark;
    }

}
