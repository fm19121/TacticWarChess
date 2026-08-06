using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [SerializeField]
    protected PawnMovement pawnMovement;
    [SerializeField]
    protected PawnAttack pawnAttack;

    protected GridPosition gridPos;

    protected List <BaseAction> pawnActions = new List<BaseAction>();
    //parameters
    protected float baseAttack;
    protected float currentAttack;

    protected float counterAttack;

    protected float baseDefense;
    protected float currentDefense;

    protected float maxHealth;
    protected float health;

    protected int attackRange;
    protected int moveRange;

    [SerializeField]
    protected bool isEnemy;

    protected bool hasShield;

    protected int actionPoint;

    protected ElementType elementType;
    protected ElementType affectedElement;
    protected ElementType imposeElement;

    protected List<BuffObject> buffs;

    public event Action OnTakenDamage;
    private event EventHandler OnPawnDead;

    public static event EventHandler OnPawnCreated;

    public static event EventHandler OnPawnDestroyed;
    
    protected void Awake()
    {
        pawnMovement = GetComponent<PawnMovement>();
        pawnAttack = GetComponent<PawnAttack>();
        BaseAction[] enabledActions = GetComponents<BaseAction>();
        foreach (BaseAction action in enabledActions)
        {
            if (action.enabled)
            {
                pawnActions.Add(action);
            }
        }
        buffs = new List<BuffObject>();
    }
    protected void Start()
    {
        gridPos = GridLevel.Instance.GetGridPosition(transform.position);
        GridLevel.Instance.AddPawnAtGrid(gridPos, this);
        actionPoint = 2;
        hasShield = false;
        affectedElement = ElementType.None;
        imposeElement = ElementType.None;

        TurnManager.Instance.ToNextTurn += ResetActionPoint;
        OnPawnDead += DeletePawn;
        OnPawnCreated?.Invoke(this, EventArgs.Empty);
    }

    protected void Update()
    {
        GridPosition newGridPos = GridLevel.Instance.GetGridPosition(transform.position);
        if (newGridPos != null)
        {
            GridLevel.Instance.RemovePawnAtGrid(gridPos);
            gridPos = newGridPos;
            GridLevel.Instance.AddPawnAtGrid(gridPos, this);
        }
    }

    public ElementType GetElementType()
    {
        return elementType;
    }
    public ElementType GetAffectedElement()
    {
        return affectedElement;
    }

    public void ChangeAffectedElement(ElementType element)
    {
        if(affectedElement != element)
        {
            affectedElement = element;
        }
    }
    public void SetHealth(float newHealth)
    {
        this.health = newHealth;
        if(newHealth <= 0)
        {
            this.health = 0;
            OnPawnDead?.Invoke(this, EventArgs.Empty);
        }
        OnTakenDamage?.Invoke();
    }

    public float GetHealth()
    {
        return this.health;
    }

    public float GetMaxHealth()
    {
        return this.maxHealth;
    }

    public float GetBaseAttack()
    {
        return this.baseAttack;
    }

    public float GetCurrentAttack()
    {
        return this.currentAttack;
    }

    public void SetCurrentAttack(float newAttack)
    {
        this.currentAttack = newAttack;
    }

    public float GetBaseDefense()
    {
        return this.baseDefense;
    }

    public float GetCurrentDefense()
    {
        return this.currentDefense;
    }

    public void SetCurrentDefense(float newDefense)
    {
        this.currentDefense = newDefense;
    }
    public virtual float GetCounterAttack()
    {
        return this.counterAttack;
    }

    public PawnMovement GetPawnMovement()
    {
        return pawnMovement;
    }

    public PawnAttack GetPawnAttack()
    {
        return pawnAttack;
    }

    public List<BaseAction> GetPawnActions()
    {
        return pawnActions;
    }

    public GridPosition GetGridPosition()
    {
        return gridPos;
    }

    public virtual int GetMoveRange()
    {
        return moveRange;
    }

    public virtual int GetAttackRange()
    {
        return attackRange;
    }

    public void SetAttackRange(int newAttackRange)
    {
        attackRange = newAttackRange;
    }

    public int GetActionPoint()
    {
        return actionPoint;
    }
    public bool IsEnemy()
    {
        return isEnemy;
    }
    public void SetShieldInfo(bool hasShield)
    {
        this.hasShield = hasShield;
    }

    public bool HasShield()
    {
        return hasShield;
    }
    public void AddBuff(BuffObject buff)
    {
        buffs.Add(buff);
    }

    public void RemoveBuff(BuffObject buff)
    {
        buffs.Remove(buff);
    }

    private void DeletePawn(object sender, EventArgs e)
    {
        OnPawnDestroyed?.Invoke(sender, e);
        GameObject.Destroy(gameObject);
    }
    
    public ElementType GetImposeElement()
    {
        return imposeElement;
    }

    public void SetImposeElement(ElementType element)
    {
        imposeElement = element;
    }
    public List<BuffObject> GetPawnBuffs()
    {
        return buffs;
    }
    public void SpendActionPoint(int actionCost)
    {
        actionPoint -= actionCost;
    }

    protected virtual void ResetActionPoint()
    {
       // if(IsEnemyInTurn() || IsPlayerInTurn())
        //{
            actionPoint = 2;
       // }
    }
}
