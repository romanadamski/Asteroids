public class DeathState : State
{
    public DeathState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        GameplayManager.Instance.ClearGameplay();

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
        GameplayManager.Instance.DestroyPlayer();
        GameplayManager.Instance.SaveScore();
        _stateMachine.SetState(GameplayManager.Instance.LoseState);
    }
}
