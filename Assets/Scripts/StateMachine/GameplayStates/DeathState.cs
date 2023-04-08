public class DeathState : State
{
    public DeathState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
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
        GameplayManager.Instance.ClearGameplay();

        GameplayManager.Instance.DestroyPlayer();
        GameplayManager.Instance.SaveScore();
        _stateMachine.SetState(GameplayManager.Instance.LoseState);
    }
}
