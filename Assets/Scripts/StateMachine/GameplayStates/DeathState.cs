using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public DeathState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    protected override void OnEnter()
    {
        ObjectPoolingManager.Instance.ReturnAllToPools();
        GameplayManager.Instance.DeactivatePlayer();
        GameplayManager.Instance.DestroyAllPlayerObjects();
        AsteroidsManager.Instance.StopReleasingAsteroidsCoroutine();

        if (GameplayManager.Instance.PlayerLivesCount == 0)
        {
            OnPlayerLose();
        }
        else
        {
            _stateMachine.SetState(GameplayManager.Instance.IdleState);
        }
    }

    private void OnPlayerLose()
    {
        GameplayManager.Instance.SaveScore();
        _stateMachine.SetState(GameplayManager.Instance.LoseState);
    }
}
