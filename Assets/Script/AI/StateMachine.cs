using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public StateData stateData;
    public State currentState;

    Dictionary<StateName, State> states;
    public StateMachine(StateData stateData)
    {
        this.stateData = stateData;
        this.states = new Dictionary<StateName, State>();
    }

    public void Transition(StateName stateName)
    {
        currentState = states[stateName];
        currentState.OnAwake();
        Debug.Log("Current State " + currentState);
    }
    public void AddState(StateName stateName, State state)
    {
        states.Add(stateName, state);
    }
    public void Update()
    {
        currentState.Update();
    }
}
