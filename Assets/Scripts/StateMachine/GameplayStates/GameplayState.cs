public class GameplayState : StateWithMenu<GameplayMenu>
{
    public GameplayState(StateMachine stateMachine) : base(stateMachine) { }

    protected override void OnEnter()
    {
        GameplayManager.Instance.StartGameplay();
        base.OnEnter();
    }
}
