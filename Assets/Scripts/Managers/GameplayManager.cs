using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : BaseManager<GameplayManager>
{
    private MortalPlayerController _playerInstance;
    private List<GameObject> _playerObjects = new List<GameObject>();

    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public WinState WinState { get; private set; }
    public LoseState LoseState { get; private set; }
    public DeathState DeathState { get; private set; }
    public IdleState IdleState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

    #endregion

    public uint CurrentScore { get; private set; }
    public uint PlayerLivesCount => _playerInstance.LivesCount;

    private void Awake()
    {
        InitStates();
    }

    private void Start()
    {
        SubscribeToEvents();
    }

    private void InitStates()
    {
        _gameplayStateMachine = gameObject.AddComponent<StateMachine>();

        GameplayState = new GameplayState(_gameplayStateMachine);
        WinState = new WinState(_gameplayStateMachine);
        LoseState = new LoseState(_gameplayStateMachine);
        DeathState = new DeathState(_gameplayStateMachine);
        IdleState = new IdleState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.AsteroidShotted += AsteroidShotted;
    }

    public void SetDeathState()
    {
        _gameplayStateMachine.SetState(DeathState);
    }

    public void SaveScore()
    {
        if (CurrentScore > 0)
        {
            SaveManager.Instance.SetHighscore(CurrentScore);
            SaveManager.Instance.Save();
        }
    }

    public void ClearGameplay()
    {
        ObjectPoolingManager.Instance.ReturnAllToPools();
        DeactivatePlayer();
        DestroyAllPlayerObjects();
        AsteroidReleasingManager.Instance.StopReleasingAsteroidsCoroutine();
    }

    private void AsteroidShotted()
    {
        IncrementScore();
    }

    public void StartCurrentLevel()
    {
        ResetScore();
        LevelSettingsManager.Instance.SetCurrentLevel();

        SpawnPlayer();

        EventsManager.Instance.OnLevelStarted(LevelSettingsManager.Instance.CurrentLevelNumber);
    }

    private void IncrementScore()
    {
        CurrentScore += GameSettingsManager.Instance.Settings.AsteroidShottedPoints;
        EventsManager.Instance.OnScoreUpdated(CurrentScore);
    }

    private void ResetScore()
    {
        CurrentScore = 0;
        EventsManager.Instance.OnScoreUpdated(CurrentScore);
    }

    public void StartGameplay()
    {
        ActivatePlayer();
        SpawnAllPlayerObjects();
        AsteroidReleasingManager.Instance.StartReleasingAsteroidCoroutine();
    }

    private void SpawnPlayer()
    {
        _playerInstance = Instantiate(LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ObjectPrefab, GameLauncher.Instance.GamePlane.transform).GetComponent<MortalPlayerController>();
    }

    private void ActivatePlayer()
    {
        if (!_playerInstance) return;

        _playerInstance.gameObject.SetActive(true);
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

    public void SetGameplayState()
    {
        _gameplayStateMachine.SetState(GameplayState);
    }

    public void SetEndGameplayState()
    {
        _gameplayStateMachine.SetState(EndGameplayState);
    }

    public void ClearGameplayStateMachine()
    {
        ClearGameplay();
        DestroyPlayer();
        _gameplayStateMachine.Clear();
    }

    public void DeactivatePlayer()
    {
        if (_playerInstance == null) return;

        _playerInstance.gameObject.SetActive(false);
    }

    public void DestroyPlayer()
    {
        if (_playerInstance == null) return;

        Destroy(_playerInstance.gameObject);
    }

    public void DestroyAllPlayerObjects()
    {
        foreach (var playerObject in _playerObjects)
        {
            Destroy(playerObject);
        }
        _playerObjects.Clear();
    }

    public void PauseGameplay()
    {
        InputManager.Instance.ToggleInput(false);
        Time.timeScale = 0;
    }

    public void ResumeGameplay()
    {
        InputManager.Instance.ToggleInput(true);
        Time.timeScale = 1;
    }

    private void UnsubscribeFromEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.AsteroidShotted -= AsteroidShotted;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
