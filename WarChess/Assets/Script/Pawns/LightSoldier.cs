using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSoldier : Pawn
{
    protected new void Start()
    {
        base.Start();
        elementType = ElementType.Light;

        baseAttack = 58f;
        currentAttack = baseAttack;

        counterAttack = 40f;

        baseDefense = 30f;
        currentDefense = baseDefense;

        maxHealth = 120f;
        health = maxHealth;

        attackRange = 1;
        moveRange = 1;
    }

    protected new void Update()
    {
        base.Update();
    }
    
    public override int GetAttackRange()
    {
        ElementType element = EnvironmentSystem.Instance.GetCurrentEnvironmentElement();
        if (affectedElement != ElementType.None)
        {
            element = affectedElement;
        }
        if (element == ElementType.Dark)
        {
            attackRange = 1;
        }
        else if (element == ElementType.Light)
        {
            attackRange = 2;
        }
        return attackRange;
    }

    public override float GetCounterAttack()
    {
        ElementType element = EnvironmentSystem.Instance.GetCurrentEnvironmentElement();
        if (affectedElement != ElementType.None)
        {
            element = affectedElement;
        }
        if (element == ElementType.Dark)
        {
            counterAttack = 30;
        }
        else if (element == ElementType.Light)
        {
            counterAttack = 40;
        }
        return counterAttack;
    }
}
