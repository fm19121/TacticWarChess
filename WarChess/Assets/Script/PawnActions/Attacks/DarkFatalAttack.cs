using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFatalAttack : PawnAttack
{
    private const float ATTACK_TIME = 1.0f;
    protected override void Awake()
    {
        base.Awake();
    }
    protected new void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        SetActionCost(1);
        battleManager = GetComponent<BattleManager>();
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        actionTime += Time.deltaTime;

        if (actionTime >= ATTACK_TIME)
        {
            SetNotActive();
            PawnActionManager.Instance.FinishAction();
        }
    }

    public override void TakeAction(GridPosition gridPos)
    {
        SetIsActive();
        actionTime = 0.0f;
        Pawn enemyPawn = GridLevel.Instance.GetPawnAtGrid(gridPos);
        battleManager.TakeDamage(enemyPawn);
        pawn.GetPawnAttack().InvokeOnAttack();
        if (enemyPawn is LightHealer)
        {
            return;
        }
    }

    protected override bool IsValidActionRegion(GridPosition gridPos)
    {
        return GridLevel.Instance.IsValidRegion(gridPos) && GridLevel.Instance.IsEnemyInRegion(gridPos, pawn);
    }
    public override List<GridPosition> GetActionRegion()
    {
        List<GridPosition> attackableRegion = new List<GridPosition>();
        int attackRange = pawn.GetAttackRange() + 1;
        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int z = -attackRange; z <= attackRange; z++)
            {
                GridPosition positionOffset = new GridPosition(x, z);
                GridPosition attackacblePos = pawn.GetGridPosition() + positionOffset;

                if (!IsValidActionRegion(attackacblePos))
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
        int attackRange = pawn.GetAttackRange() + 1;
        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int z = -attackRange; z <= attackRange; z++)
            {
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
        return "FatalAttack";
    }
}
