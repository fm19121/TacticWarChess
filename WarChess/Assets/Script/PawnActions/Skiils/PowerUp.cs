using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : BaseAction
{
    private const float Skill_Time = 0.5f;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SetActionCost(1);
    }
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        actionTime += Time.deltaTime;

        if (actionTime >= Skill_Time)
        {
            SetNotActive();
            PawnActionManager.Instance.FinishAction();
        }
    }
    public override void TakeAction(GridPosition gridPos)
    {
        SetIsActive();
        actionTime = 0.0f;
        BuffObject fatalDarkAttack = new BuffObject(BuffObject.BuffName.FatalDarkAttack);
        pawn.AddBuff(fatalDarkAttack);
    }

    protected override bool IsValidActionRegion(GridPosition gridPos)
    {
        return GridLevel.Instance.IsValidRegion(gridPos);
    }

    public override List<GridPosition> GetActionRegion()
    {
        List<GridPosition> actionRegion = new List<GridPosition>();

        GridPosition pawnPos = pawn.GetGridPosition();

        actionRegion.Add(pawnPos);

        return actionRegion;
    }

    public override List<GridPosition> GetActionRangeRegion()
    {
        List<GridPosition> actionRegion = new List<GridPosition>();

        GridPosition pawnPos = pawn.GetGridPosition();

        actionRegion.Add(pawnPos);

        return actionRegion;
    }

    public override int GetActionCost()
    {
        ElementType elementType = pawn.GetAffectedElement();
        if(elementType == ElementType.None)
        {
            elementType = EnvironmentSystem.Instance.GetCurrentEnvironmentElement();
        }
        if(elementType == ElementType.Light)
        {
            return 100;
        }
        return 1;
    }

    public override string GetActionName()
    {
        return "PowerUP";
    }
}
