using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class Wait : State
{
    public Wait(StateMachine machine) : base(machine)
    {

    }

    public override void OnAwake()
    {
        time = 0.0f;
        Debug.Log("AT Wait");
    }


    public override void Update()
    {
        time += Time.deltaTime;
        if (!TurnManager.Instance.IsPlayerTurn() && time >= 1.5f)
        {
            machine.Transition(StateName.Select);
        }
    }
}
