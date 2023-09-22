using System.Collections;
using UnityEngine;

public class IdleState : State
{
    private IdleMenu _idleMenu;
    private Coroutine _idleCoroutine;

    public IdleState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        if (_idleMenu || UIManager.Instance.TryGetMenuByType(out _idleMenu))
        {
            _idleMenu.Show();
        }
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

        if (_idleMenu || UIManager.Instance.TryGetMenuByType(out _idleMenu))
        {
            _idleMenu.Hide();
        }
    }
}
