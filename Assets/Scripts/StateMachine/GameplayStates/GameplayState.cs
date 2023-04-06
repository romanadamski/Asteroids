using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : State
{
    private GameplayMenu _gameplayMenu;

    public GameplayState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    protected override void OnEnter()
    {
        GameplayManager.Instance.SetCurrentLevel();
        if (_gameplayMenu || UIManager.Instance.TryGetMenuByType(out _gameplayMenu))
        {
            _gameplayMenu.Show();
        }
    }

    protected override void OnExit()
    {
        GameplayManager.Instance.EndCurrentLevel();

        if (_gameplayMenu || UIManager.Instance.TryGetMenuByType(out _gameplayMenu))
        {
            _gameplayMenu.Hide();
        }
    }
}
