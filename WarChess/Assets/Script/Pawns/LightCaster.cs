using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class LightCaster : Pawn
{
    protected new void Start()
    {
        base.Start();
        elementType = ElementType.Light;

        baseAttack = 40f;
        currentAttack = baseAttack;

        baseDefense = 20f;
        currentDefense = baseDefense;

        counterAttack = 26f;

        maxHealth = 90f;
        health = maxHealth;

        attackRange = 4;
        moveRange = 2;

        imposeElement = ElementType.Light;

        /*TurnManager.Instance.ToNextTurn += ChangeAttackRange;
        OnAffectedElementChange += ChangeAttackRange;*/
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
            attackRange = 4;
        }
        return attackRange;
    }
   /* private void ChangeAttackRange()
    {
        ElementType element = EnvironmentSystem.Instance.GetCurrentEnvironmentElement();
        if (affectedElement != ElementType.None)
        {
            element = affectedElement;
        }
        if (element == ElementType.Dark)
        {
            attackRange -= 2;
        }
        else if (element == ElementType.Light)
        {
            attackRange += 2;
        }
    }*/

}
