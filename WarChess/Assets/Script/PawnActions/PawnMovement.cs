using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : BaseAction
{

    [SerializeField]
    private float moveSpeed = 10.0f;
    private float stopRadius = 0.1f;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        if (Vector3.Distance(transform.position, targetPosition) > stopRadius)
        {
            Vector3 moveDir = (targetPosition - transform.position).normalized;
            transform.position += moveDir * Time.deltaTime * moveSpeed;
        }
        else
        {
            SetNotActive();
            PawnActionManager.Instance.FinishAction();
        }

    }

    //Move
    public override void TakeAction(GridPosition gridPos)
    {
        SetIsActive();
        targetPosition = GridLevel.Instance.GetWorldPosition(gridPos);
    }

    protected override bool IsValidActionRegion(GridPosition gridPos)
    {
        return GridLevel.Instance.IsValidRegion(gridPos) && !GridLevel.Instance.IsPawnInRegion(gridPos);
    }
    public override List<GridPosition> GetActionRegion()
    {
        List<GridPosition> reachableRegion = new List<GridPosition>();
        int moveRange = pawn.GetMoveRange();
        for(int x = -moveRange; x <= moveRange; x++)
        {
            for(int z = -moveRange; z <= moveRange; z++)
            {
                GridPosition positionOffset = new GridPosition(x, z);
                GridPosition reachacblePos = pawn.GetGridPosition() + positionOffset;
                if (!IsValidActionRegion(reachacblePos))
                {
                    continue;
                }
                if(positionOffset == new GridPosition(0, 0))
                {
                    continue;
                }
                reachableRegion.Add(reachacblePos);
            }
        }
        return reachableRegion;
    }

    public override List<GridPosition> GetActionRangeRegion()
    {
        List<GridPosition> reachableRegion = new List<GridPosition>();
        int moveRange = pawn.GetMoveRange();
        for (int x = -moveRange; x <= moveRange; x++)
        {
            for (int z = -moveRange; z <= moveRange; z++)
            {
                GridPosition positionOffset = new GridPosition(x, z);
                GridPosition reachacblePos = pawn.GetGridPosition() + positionOffset;
                if (!GridLevel.Instance.IsValidRegion(reachacblePos))
                {
                    continue;
                }
                if (positionOffset == new GridPosition(0, 0))
                {
                    continue;
                }
                reachableRegion.Add(reachacblePos);
            }
        }
        return reachableRegion;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
