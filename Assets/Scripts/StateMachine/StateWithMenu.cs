public abstract class StateWithMenu<T> : State where T : BaseMenu
{
    public StateWithMenu(StateMachine stateMachine) : base(stateMachine) { }

    protected T menu; 

    protected override void OnEnter()
    {
        if (menu || UIManager.Instance.TryGetMenuByType(out menu))
        {
            menu.Show();
        }
    }

    protected override void OnExit()
    {
        if (menu || UIManager.Instance.TryGetMenuByType(out menu))
        {
            menu.Hide();
        }
    }
}
