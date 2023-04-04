using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : State
{
    private BaseMenu _mainMenu;

    public MainMenuState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    protected override void OnEnter()
    {
        if (_mainMenu || UIManager.Instance.TryGetMenuByType<MainMenu>(out _mainMenu))
        {
            _mainMenu.Show();
        }
    }

    protected override void OnExit()
    {
        if (_mainMenu || UIManager.Instance.TryGetMenuByType<MainMenu>(out _mainMenu))
        {
            _mainMenu.Hide();
        }
    }

    protected override void OnUpdate()
    {

    }
}
