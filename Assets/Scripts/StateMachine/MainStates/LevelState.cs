using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : State
{
    public LevelState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    protected override void OnEnter()
    {
        GameplayManager.Instance.StartGameplay();
    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate()
    {

    }
}
