using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class DarkAssasin : Pawn
{
    protected new void Start()
    {
        base.Start();
        elementType = ElementType.Dark;

        baseAttack = 25;
        currentAttack = baseAttack;

        counterAttack = 20;

        baseDefense = 20;
        currentDefense = baseDefense;

        maxHealth = 880f;
        health = maxHealth;

        attackRange = 1;
        moveRange = 2;
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
            attackRange = 2;
        }
        else if (element == ElementType.Light)
        {
            attackRange = 1;
        }
        return attackRange;
    }
}
