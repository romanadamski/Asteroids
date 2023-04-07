using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private IdleMenu _idleMenu;

    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    protected override void OnEnter()
    {
        if (_idleMenu || UIManager.Instance.TryGetMenuByType(out _idleMenu))
        {
            _idleMenu.Show();
        }
        GameplayManager.Instance.StartCoroutine(IdleCoroutine());
    }

    private IEnumerator IdleCoroutine()
    {
        yield return new WaitForSeconds(GameSettingsManager.Instance.Settings.IdleStateTime);
        GameplayManager.Instance.SetGameplayState();
    }

    protected override void OnExit()
    {
        if (_idleMenu || UIManager.Instance.TryGetMenuByType(out _idleMenu))
        {
            _idleMenu.Hide();
        }
    }
}
