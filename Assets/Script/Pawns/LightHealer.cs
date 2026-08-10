using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHealer : Pawn
{
    protected new void Start()
    {
        base.Start();
        elementType = ElementType.Light;

        baseAttack = 45f;
        currentAttack = baseAttack;

        counterAttack = 0f;

        baseDefense = 16f;
        currentDefense = baseDefense;

        maxHealth = 100f;
        health = maxHealth;

        attackRange = 6;
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
            attackRange = 3;
        }
        else if (element == ElementType.Light)
        {
            attackRange = 6;
        }
        return attackRange;
    }
}
