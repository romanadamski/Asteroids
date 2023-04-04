using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class State
{
    protected StateMachine _stateMachine;
 
    protected virtual void OnEnter()
    {

    }
    protected virtual void OnUpdate()
    {

    }
    protected virtual void OnExit()
    {

    }

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.Log($"OnEnter: {_stateMachine.CurrentState.GetType().Name}".Color("green"));
        OnEnter();
    }

    public void Update()
    {
        OnUpdate();
    }

    public void Exit()
    {
        Debug.Log($"OnExit: {_stateMachine.CurrentState.GetType().Name}".Color("red"));
        OnExit();
    }
}
