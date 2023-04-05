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
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        gameStateMachine.SetState(MainMenuState);
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
}
