using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObject
{
    public enum BuffName
    {
        LightArrow, //lightArcher
        FatalDarkAttack,//darkAssasin
        DarkShield, //darkCaster
        LightEnhancement, // lightCaster
        LightBurn,

    }
    BuffName buffName;
    private float attackRatio = 0;
    private float defenseRatio = 0;

    private bool hasShield = false;

    private int duration = 0;
    private int useTime = 0;

    private ElementType imposeElement = ElementType.None;
    private ElementType receiveElement = ElementType.None;

    public BuffObject(BuffName name)
    {
        this.buffName = name;
        switch(name)
        {
            case BuffName.LightArrow:
                LightArrow();
                break;
            case BuffName.FatalDarkAttack:
                FatalDarkAttack();
                break;
            case BuffName.DarkShield:
                DarkShield(); 
                break;
            case BuffName.LightEnhancement:
                LighEnhancement(); 
                break;
            case BuffName.LightBurn:
                LightBurn();
                break;
        }
    }

    private void LightArrow()
    {
        this.attackRatio = 0.5f;
        this.defenseRatio = -0.5f;
        this.duration = 2;
        this.useTime = 1;
        this.imposeElement = ElementType.Light;
    }

    private void FatalDarkAttack()
    {
        this.attackRatio = 0.8f;
        this.duration = 1000;
        this.useTime = 1;
        this.imposeElement = ElementType.Dark;
    }

    private void DarkShield()
    {
        this.hasShield = true; ;
        this.duration = 2;
        this.useTime = 1;
        this.receiveElement = ElementType.Dark;
    }

    private void LighEnhancement()
    {
        this.attackRatio = 0.2f;
        this.defenseRatio = 0.5f;
        this.receiveElement = ElementType.Light;
        this.duration = 4;
        this.useTime = 100;
    }

    private void LightBurn()
    {
        this.defenseRatio = -0.4f;
        this.receiveElement = ElementType.Light;
        this.duration = 2;
        this.useTime = 100;
    }

    public override bool Equals(object obj)
    {
        return obj is BuffObject buff && this.buffName == buff.buffName;
    }

    public bool Equals(BuffObject other)
    {
        return this.buffName == other.buffName;
    }

    public override int GetHashCode()
    {
        return (int)buffName;
    }

    public static bool operator ==(BuffObject a, BuffObject b)
    {
        return a.buffName == b.buffName;
    }

    public static bool operator !=(BuffObject a, BuffObject b)
    {
        return a.buffName != b.buffName;
    }
    public int GetDuration()
    {
        return duration;
    }

    public int GetUseTime()
    {
        return useTime;
    }

    public void ReduceDuration()
    {
        duration -= 1;
        if(duration < 0)
        {
            duration = 0;
        }
    }

    public void ReduceUseTime()
    {
        useTime -= 1;
        if (useTime < 0)
        {
            useTime = 0;
        }
    }

    public float GetAttackRatio()
    {
        return attackRatio;
    }

    public float GetDefenseRatio()
    {
        return defenseRatio;
    }

    public bool HasShield()
    {
        return hasShield;
    }
    public ElementType GetImposeElement()
    {
        return imposeElement;
    }

    public ElementType GetReceiveElement()
    {
        return receiveElement;
    }
}
