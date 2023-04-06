using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : BaseManager<GameplayManager>
{
    private GameObject _playerInstance;
    private List<GameObject> _playerObjects = new List<GameObject>();

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

    private void Start()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerLoseLife += PlayerLoseLife;
    }

    private void UnsubscribeFromEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.PlayerLoseLife -= PlayerLoseLife;
    }

    private void PlayerLoseLife(uint lives)
    {
        if (lives == 0)
        {
            SetLoseGameplayState();
        }
    }

    private void InitStates()
    {
        _gameplayStateMachine = gameObject.AddComponent<StateMachine>();

        GameplayState = new GameplayState(_gameplayStateMachine);
        WinState = new WinState(_gameplayStateMachine);
        LoseState = new LoseState(_gameplayStateMachine);
        DeathState = new DeathState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    public void SetCurrentLevel()
    {
        LevelSettingsManager.Instance.SetCurrentLevel();
        SpawnPlayer();
        SpawnAllPlayerObjects();

        EventsManager.Instance.OnGameplayStarted(LevelSettingsManager.Instance.CurrentLevelNumber);
    }

    private void SpawnPlayer()
    {
        _playerInstance = Instantiate(LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ObjectPrefab, GameLauncher.Instance.GamePlane.transform);
        _playerInstance.SetActive(true);
        _playerInstance.transform.position = LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ObjectStartPosition;
        _playerInstance.transform.rotation = LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ObjectStartRotation;
    }

    private void SpawnAllPlayerObjects()
    {
        foreach (var playerObject in LevelSettingsManager.Instance.CurrentLevel.PlayerObjects)
        {
            var playerObjectInstance = Instantiate(playerObject.ObjectPrefab, GameLauncher.Instance.GamePlane.transform);
            playerObjectInstance.SetActive(true);
            playerObjectInstance.transform.position = playerObject.ObjectStartPosition;
            playerObjectInstance.transform.rotation = playerObject.ObjectStartRotation;
            _playerObjects.Add(playerObjectInstance);
        }
    }

    public void StartGameplay()
    {
        _gameplayStateMachine.SetState(GameplayState);
    }

    public void SetLoseGameplayState()
    {
        _gameplayStateMachine.SetState(LoseState);
    }

    public void SetEndGameplayState()
    {
        _gameplayStateMachine.SetState(EndGameplayState);
    }

    public void EndGameplay()
    {
        _gameplayStateMachine.Clear();
    }

    public void EndCurrentLevel()
    {
        ObjectPoolingManager.Instance.ReturnAllToPool();
        DespawnPlayer();
        DespawnAllPlayerObjects();
    }

    private void DespawnPlayer()
    {
        if (_playerInstance == null) return;

        _playerInstance.SetActive(false);
    }

    private void DespawnAllPlayerObjects()
    {
        foreach (var playerObject in _playerObjects)
        {
            Destroy(playerObject);
        }
        _playerObjects.Clear();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
