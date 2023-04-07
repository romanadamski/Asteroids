public class LoseState : State
{
    private LoseMenu _loseMenu;

    public LoseState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    protected override void OnEnter()
    {
        if (_loseMenu || UIManager.Instance.TryGetMenuByType(out _loseMenu))
        {
            _loseMenu.Show();
        }
    }

    protected override void OnExit()
    {
        if (_loseMenu || UIManager.Instance.TryGetMenuByType(out _loseMenu))
        {
            _loseMenu.Hide();
        }
    }
}
