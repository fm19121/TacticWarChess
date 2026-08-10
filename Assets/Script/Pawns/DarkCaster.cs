using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class DarkCaster : Pawn
{
    protected new void Start()
    {
        base.Start();
        elementType = ElementType.Dark;

        baseAttack = 42f;
        currentAttack = baseAttack;

        baseDefense = 18f;
        currentDefense = baseDefense;

        counterAttack = 28f;

        maxHealth = 85f;
        health = maxHealth;

        attackRange = 4;
        moveRange = 2;

        imposeElement = ElementType.Dark;
        affectedElement = ElementType.None;
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
            attackRange = 4;
        }
        else if (element == ElementType.Light)
        {
            attackRange = 2;
        }
        return attackRange;
    }
}
