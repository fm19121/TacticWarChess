using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Pawn pawn;
    protected Vector3 targetPosition;

    [SerializeField]protected int actionCost = 1;

    protected float actionTime;

    protected bool isActive;
    protected virtual void Awake()
    {
        pawn = GetComponent<Pawn>();
        targetPosition = transform.position;
        isActive = false;
    }
    void Start()
    {
        
    }

    protected abstract bool IsValidActionRegion(GridPosition gridPos);

    public abstract List<GridPosition> GetActionRegion();

    public abstract List<GridPosition> GetActionRangeRegion();

    public bool IsInActionRegion(GridPosition gridPos)
    {
        return GetActionRegion().Contains(gridPos);
    }

    public bool CanTakeAction()
    {
        return pawn.GetActionPoint() >= GetActionCost() && TurnManager.Instance.GetTurnActionPoint() >= GetActionCost() && !isActive;
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPos);

    public virtual int GetActionCost()
    {
        return actionCost;
    }

    public void SetActionCost(int cost)
    {
        actionCost = cost;
    }

    protected void SetIsActive()
    {
        isActive = true;
    }

    protected void SetNotActive()
    {
        isActive = false;
    }

    public Pawn GetPawn()
    {
        return pawn;
    }
}
