using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    Pawn pawn;

    public static event Action OnBuffRemoved;

    void Start()
    {
        pawn = GetComponent<Pawn>();
        PawnAttack attack = pawn.GetPawnAttack();
        TurnManager.Instance.ToNextTurn += RemovePawnBuffsInTurn;
        attack.OnAttack += RemovePawnBuffsInUse;
        PawnActionManager.Instance.OnSpenActionPoint += UpdatePawnParams;
    }

    void Update()
    {
        
    }

    private void RemovePawnBuffsInTurn()
    {
        List<BuffObject> pawnBuffs = pawn.GetPawnBuffs();
        int buffNum = pawnBuffs.Count;
        if(buffNum < 1)
        {
            return;
        }
        for(int i = buffNum - 1; i >= 0; i--)
        {
            pawnBuffs[i].ReduceDuration();
            if (pawnBuffs[i].GetDuration() <= 0 || pawnBuffs[i].GetUseTime() <= 0)
            {
                if (pawnBuffs[i].GetImposeElement() != ElementType.None)
                {
                    pawn.SetImposeElement(ElementType.None);
                }
                pawnBuffs.RemoveAt(i);
                OnBuffRemoved?.Invoke();
            }
        }
        UpdatePawnParams();
    }

    private void RemovePawnBuffsInUse()
    {
        List<BuffObject> pawnBuffs = pawn.GetPawnBuffs();
        int buffNum = pawnBuffs.Count;
        if(buffNum < 1)
        {
            return;
        }
        for(int i = buffNum - 1; i >= 0; i--)
        {
            pawnBuffs[i].ReduceUseTime();
            if (pawnBuffs[i].GetDuration() <= 0 || pawnBuffs[i].GetUseTime() <= 0)
            {
                if (pawnBuffs[i].GetImposeElement() != ElementType.None)
                {
                    pawn.SetImposeElement(ElementType.None);
                }
                pawnBuffs.RemoveAt(i);
                OnBuffRemoved?.Invoke();
            }
        }
        UpdatePawnParams();
    }

    public void UpdatePawnParams()
    {
        List<BuffObject> pawnBuffs = pawn.GetPawnBuffs();
        float baseAttack = pawn.GetBaseAttack();
        float baseDefense = pawn.GetBaseDefense();
        float currentAttack = baseAttack;
        float currentDefense = baseDefense;
        bool hasShield = false;
        foreach (BuffObject buff in pawnBuffs)
        {
            currentAttack += baseAttack * buff.GetAttackRatio();
            currentDefense += baseDefense * buff.GetDefenseRatio();
            if(buff.GetImposeElement() != ElementType.None)
            {
                pawn.SetImposeElement(buff.GetImposeElement());
            }
            if(buff.GetReceiveElement() != ElementType.None)
            {
                pawn.ChangeAffectedElement(buff.GetReceiveElement());
            }
            hasShield = buff.HasShield();
        }
        pawn.SetCurrentAttack(currentAttack);
        pawn.SetCurrentDefense(currentDefense);
        pawn.SetShieldInfo(hasShield);
    }

    public void TakeDamage(Pawn enemyPawn)
    {
        if (enemyPawn.HasShield())
        {
            enemyPawn.RemoveBuff(new BuffObject(BuffObject.BuffName.DarkShield));
            return;
        }
        float damage = pawn.GetCurrentAttack() - enemyPawn.GetCurrentDefense();
        if (damage <= 5)
        {
            damage = 5;
        }
        if(IsElmentConflict(enemyPawn))
        {
            damage = 2 * damage;
        }
        enemyPawn.SetHealth(enemyPawn.GetHealth() - damage);
        if (pawn.GetImposeElement() != ElementType.None)
        {
            enemyPawn.ChangeAffectedElement(pawn.GetImposeElement());
        }
    }

    private bool IsElmentConflict(Pawn enemyPawn)
    {
        if(enemyPawn.GetAffectedElement() == ElementType.Dark && pawn.GetImposeElement() == ElementType.Light)
        {
            return true;
        }
        if(enemyPawn.GetAffectedElement() == ElementType.Light && pawn.GetImposeElement() == ElementType.Dark)
        {
            return true;
        }
        return false;
    }

    public void CounterAttacked(Pawn enemyPawn)
    {
        float damage = enemyPawn.GetCounterAttack() - (pawn.GetCurrentDefense() * 0.6f);
        if (damage <= 0)
        {
            damage = 0;
        }
        if (IsElmentConflict(enemyPawn))
        {
            damage = 2 * damage;
        }
        pawn.SetHealth(pawn.GetHealth() - damage);
        if (pawn.GetImposeElement() != ElementType.None)
        {
            pawn.ChangeAffectedElement(enemyPawn.GetImposeElement());
        }
    }
}
