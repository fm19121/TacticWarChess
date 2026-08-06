using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBow : BaseAction
{
    private const float Skill_Time = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        SetActionCost(2);
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
        BuffObject enhancedArrow = new BuffObject(BuffObject.BuffName.LightArrow);
        pawn.AddBuff(enhancedArrow);
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

    public override string GetActionName()
    {
        return "DrawBow";
    }
}
