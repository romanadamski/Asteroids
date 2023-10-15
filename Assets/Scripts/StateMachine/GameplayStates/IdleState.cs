using System.Collections;
using UnityEngine;

public class IdleState : StateWithMenu<IdleMenu>
{
    private Coroutine _idleCoroutine;

    public IdleState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        base.OnEnter();

        GameplayManager.Instance.PauseGameplay();

        StopIdleCoroutine();
        _idleCoroutine = GameplayManager.Instance.StartCoroutine(IdleCoroutine());
    }

    private IEnumerator IdleCoroutine()
    {
        yield return new WaitForSecondsRealtime(GameSettingsManager.Instance.Settings.IdleStateTime);
        GameplayManager.Instance.SetGameplayState();
    }

    private void StopIdleCoroutine()
    {
        if (_idleCoroutine != null)
        {
            GameplayManager.Instance.StopCoroutine(_idleCoroutine);
            _idleCoroutine = null;
        }
    }

    protected override void OnExit()
    {
        StopIdleCoroutine();
        GameplayManager.Instance.ClearGameplay();

        base.OnExit();
    }
}
