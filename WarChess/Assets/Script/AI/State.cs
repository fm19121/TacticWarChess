using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateName
{
    Wait,
    Select,
    TakeAction
}

public class StateData 
{
    public Pawn selectedPawn;
    public GridPosition targetPosition;
    public BaseAction selectedAction;

    public StateData()
    {
        selectedAction = null;
        selectedAction = null;
        targetPosition = new GridPosition();
    }
}

public abstract class State
{
    protected StateMachine machine;
    protected StateData stateData;

    protected float time;

    public State(StateMachine machine)
    {
        this.machine = machine;
        this.stateData = machine.stateData;
    }
    public abstract void OnAwake();

    public abstract void Update();
}