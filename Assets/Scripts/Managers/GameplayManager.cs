using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : BaseManager<GameplayManager>
{
    [SerializeField]
    GameObject playerPrefab;
    GameObject _playerInstance;

    #region States
    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public WinState WinState { get; private set; }
    public LoseState LoseState { get; private set; }
    public DeathState DeathState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }
    #endregion

    private void Awake()
    {
        InitStates();
    }

    private void InitStates()
    {
        GameplayState = new GameplayState(_gameplayStateMachine);
        WinState = new WinState(_gameplayStateMachine);
        LoseState = new LoseState(_gameplayStateMachine);
        DeathState = new DeathState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    public void StartGameplay()
    {
        _gameplayStateMachine = new StateMachine();
        
    }

    public void SpawnPlayer()
    {
        if (_playerInstance == null)
        {
            _playerInstance = Instantiate(playerPrefab, GameLauncher.Instance.GamePlane.transform);
        }
        _playerInstance.SetActive(true);
    }

    public void DespawnPlayer()
    {
        if (_playerInstance == null) return;
        
        _playerInstance.SetActive(false);
    }
}
