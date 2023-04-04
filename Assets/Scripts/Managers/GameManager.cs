using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    #region States
    StateMachine gameStateMachine;
    public MainMenuState MainMenuState;
    public LevelState GameplayState;
    #endregion

    private void Start()
    {
        InitStates();
        SetFirstState(MainMenuState);

        //todo where to implement actions on player death
        EventsManager.Instance.PlayerDeath += OnPlayerDeath;
    }

    private void SetFirstState(State state)
    {
        gameStateMachine.SetState(state);
    }

    private void InitStates()
    {
        gameStateMachine = new StateMachine();

        MainMenuState = new MainMenuState(gameStateMachine);
        GameplayState = new LevelState(gameStateMachine);
    }

    public void StartGameplay()
    {
    }

    private void OnPlayerDeath(uint livesCount)
    {
    }
}
