using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class State
{
    protected abstract void OnEnter();
    protected abstract void OnUpdate();
    protected abstract void OnExit();

    protected StateMachine _stateMachine;

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
