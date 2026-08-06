using System;
using System.Collections.Generic;
using UnityEngine;

public class PawnAttack : BaseAction
{

    protected ElementType attackElement = ElementType.None;

    protected BattleManager battleManager;

    public event Action OnAttack;

    private const float ATTACK_TIME = 0.8f;
    protected override void Awake()
    {
        base.Awake();
    }
    protected void Start()
    {
        battleManager = GetComponent<BattleManager>();
        actionTime = 0.0f;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        actionTime += Time.deltaTime;

        if(actionTime >= ATTACK_TIME)
        {
            SetNotActive();
            PawnActionManager.Instance.FinishAction();
            Debug.Log("Finish Attak");
        }
    }
    //attack
    public override void TakeAction(GridPosition gridPos)
    {
        Debug.Log("StartAttack");
        SetIsActive();
        actionTime = 0.0f;
        Pawn enemyPawn = GridLevel.Instance.GetPawnAtGrid(gridPos);
        battleManager.TakeDamage(enemyPawn);
        if (enemyPawn is LightHealer)
        {
            return;
        }
        if (enemyPawn.GetPawnAttack().IsInActionRegion(pawn.GetGridPosition()))
        {
            battleManager.CounterAttacked(enemyPawn);
        }
    }

    protected override bool IsValidActionRegion(GridPosition gridPos)
    {
        return GridLevel.Instance.IsValidRegion(gridPos) && GridLevel.Instance.IsEnemyInRegion(gridPos, pawn);
    }
    public override List<GridPosition> GetActionRegion()
    {
        List<GridPosition> attackableRegion = new List<GridPosition>();
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
                if (positionOffset == new GridPosition(0, 0))
                {
                    continue;
                }
                attackableRegion.Add(attackacblePos);
            }
        }
        return attackableRegion;
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
                if (positionOffset == new GridPosition(0, 0))
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
        return "Attack";
    }

    public void InvokeOnAttack()
    {
        OnAttack?.Invoke();
    }
}
