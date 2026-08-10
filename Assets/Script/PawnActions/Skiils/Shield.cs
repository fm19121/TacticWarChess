using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseAction
{
    private const float Skill_Time = 0.5f;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
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
        BuffObject darkShield = new BuffObject(BuffObject.BuffName.DarkShield);
        Pawn allyPawn = GridLevel.Instance.GetPawnAtGrid(gridPos);
        allyPawn.AddBuff(darkShield);
    }

    protected override bool IsValidActionRegion(GridPosition gridPos)
    {
        return GridLevel.Instance.IsValidRegion(gridPos) && GridLevel.Instance.IsAllyInRegion(gridPos, pawn);
    }

    public override List<GridPosition> GetActionRegion()
    {
        List<GridPosition> attackRangeRegion = new List<GridPosition>();
        int attackRange = pawn.GetAttackRange();
        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int z = -attackRange; z <= attackRange; z++)
            {
                int attackDis = Mathf.Abs(x) + Mathf.Abs(z);
                if (attackDis > attackRange)
                {
                    continue;
                }
                GridPosition positionOffset = new GridPosition(x, z);
                GridPosition attackacblePos = pawn.GetGridPosition() + positionOffset;
                if (!IsValidActionRegion(attackacblePos))
                {
                    continue;
                }
                attackRangeRegion.Add(attackacblePos);
            }
        }
        return attackRangeRegion;
    }

    public override List<GridPosition> GetActionRangeRegion()
    {
        List<GridPosition> attackRangeRegion = new List<GridPosition>();
        int attackRange = pawn.GetAttackRange();
        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int z = -attackRange; z <= attackRange; z++)
            {
                int attackDis = Mathf.Abs(x) + Mathf.Abs(z);
                if (attackDis > attackRange)
                {
                    continue;
                }
                GridPosition positionOffset = new GridPosition(x, z);
                GridPosition attackacblePos = pawn.GetGridPosition() + positionOffset;
                if (!GridLevel.Instance.IsValidRegion(attackacblePos))
                {
                    continue;
                }
                attackRangeRegion.Add(attackacblePos);
            }
        }
        return attackRangeRegion;
    }

    public override string GetActionName()
    {
        return "Shield";
    }
}
