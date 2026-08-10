using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightArcher : Pawn
{
    protected new void Start()
    {
        base.Start();
        elementType = ElementType.Light;

        baseAttack = 44f;
        currentAttack = baseAttack;

        counterAttack = 30f;

        baseDefense = 25f;
        currentDefense = baseDefense;

        maxHealth = 100f;
        health = maxHealth;

        attackRange = 5;
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
            attackRange = 3;
        }
        else if (element == ElementType.Light)
        {
            attackRange = 5;
        }
        return attackRange;
    }
}
