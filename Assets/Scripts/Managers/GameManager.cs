using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    #region States
    StateMachine gameStateMachine;
    public MainMenuState MainMenuState;
    public LevelState LevelState;
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
        LevelState = new LevelState(gameStateMachine);
    }

    public void StartGameplay()
    {
        gameStateMachine.SetState(LevelState);
    }

    private void OnPlayerDeath(uint livesCount)
    {
    }
}
