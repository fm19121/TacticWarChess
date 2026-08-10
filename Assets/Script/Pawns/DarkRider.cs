using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRider : Pawn
{
    protected new void Start()
    {
        base.Start();
        elementType = ElementType.Dark;

        baseAttack = 48f;
        currentAttack = baseAttack;

        counterAttack = 25;

        baseDefense = 24;
        currentDefense = baseDefense;

        maxHealth = 140;
        health = maxHealth;

        attackRange = 1;
        moveRange = 1;
    }

    protected new void Update()
    {
        base.Update();
    }

    public override int GetMoveRange()
    {
        ElementType element = EnvironmentSystem.Instance.GetCurrentEnvironmentElement();
        if (affectedElement != ElementType.None)
        {
            element = affectedElement;
        }
        if (element == ElementType.Dark)
        {
            moveRange = 3;
        }
        else if (element == ElementType.Light)
        {
            moveRange = 1;
        }
        return moveRange;
    }
}
