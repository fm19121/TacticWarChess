using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTower : Pawn
{
    protected new void Start()
    {
        base.Start();
        elementType = ElementType.None;

        baseAttack = 45f;
        currentAttack = baseAttack;

        counterAttack = 36f;

        baseDefense = 35f;
        currentDefense = baseDefense;

        maxHealth = 150f;
        health = maxHealth;

        attackRange = 5;
        moveRange = 0;

        //isEnemy = true;
        TurnManager.Instance.ToNextTurn += ChangeImposeElement;
    }

    protected new void Update()
    {
        base.Update();
    }

    private void ChangeImposeElement()
    {
        ElementType element = EnvironmentSystem.Instance.GetCurrentEnvironmentElement();

        imposeElement = element;
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
            attackRange = 5;
        }
        return attackRange;
    }
}
