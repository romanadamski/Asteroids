using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameplayState : State
{
    public EndGameplayState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    protected override void OnEnter()
    {
        GameplayManager.Instance.EndGameplay();
    }

    protected override void OnExit()
    {
        GameManager.Instance.GoToMainMenu();
    }
}
