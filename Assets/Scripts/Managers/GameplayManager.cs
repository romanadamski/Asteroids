using System.Collections.Generic;
using UnityEngine;

struct ShipData
{
    public BaseMortalObjectController mortalShip;
    public Vector2 startPosition;

    public ShipData(BaseMortalObjectController mortalShip, Vector2 startPosition)
    {
        this.mortalShip = mortalShip;
        this.startPosition = startPosition;
    }
}

public class GameplayManager : BaseManager<GameplayManager>
{
    private GameObject _playerInstance;
    private List<ShipData> _playerShips = new List<ShipData>();
    private List<ShipData> _enemyShips = new List<ShipData>();

    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public WinState WinState { get; private set; }
    public LoseState LoseState { get; private set; }
    public IdleState IdleState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

    #endregion

    public uint CurrentScore { get; private set; }
    public bool IsPaused => Time.deltaTime == 0;

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
        IdleState = new IdleState(_gameplayStateMachine);
        EndGameplayState = new EndGameplayState(_gameplayStateMachine);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.EnemyShotted += ObjectShotted;
        EventsManager.Instance.PlayerLoseLife += PlayerLoseLife;
    }

    private void PlayerLoseLife(uint lives)
    {
        if (lives == 0)
        {
            OnPlayerLose();
        }
        else
        {
            _gameplayStateMachine.PushState(IdleState);
        }
    }

    private void OnPlayerLose()
    {
        ClearGameplay();

        DestroyPlayer();
        DestroyAllShips();
        SaveScore();
        _gameplayStateMachine.SetState(LoseState);
    }

    public void SaveScore()
    {
        if (CurrentScore > 0)
        {
            SaveManager.Instance.AddScoreToHighscores(CurrentScore);
            SaveManager.Instance.Save();
        }
    }

    public void ClearGameplay()
    {
        ObjectPoolingManager.Instance.ReturnAllToPools();
        DeactivatePlayer();
        DeactivateAllShips();
        AsteroidReleasingManager.Instance.StopReleasingAsteroidsCoroutine();
    }

    private void ObjectShotted(string tag)
    {
        IncrementScore();
    }

    public void StartCurrentLevel()
    {
        ResetScore();
        LevelSettingsManager.Instance.SetCurrentLevel();

        SpawnPlayer();
        _playerShips.AddRange(SpawnShips(LevelSettingsManager.Instance.CurrentLevel.PlayerObjects));
        _enemyShips.AddRange(SpawnShips(LevelSettingsManager.Instance.CurrentLevel.EnemyObjects));

        EventsManager.Instance.OnLevelStarted(LevelSettingsManager.Instance.CurrentLevel);
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
        ResumeGameplay();
        ActivatePlayer();
        ActivateShips(_playerShips);
        ActivateShips(_enemyShips);

        AsteroidReleasingManager.Instance.StartReleasingAsteroidCoroutine();

        EventsManager.Instance.OnGameplayStarted();
    }

    private List<ShipData> SpawnShips(List<ShipObject> shipObjects)
    {
        var ships = new List<ShipData>();
        foreach (var ship in shipObjects)
        {
            var shipInstance = Instantiate(ship.ObjectPrefab, GameLauncher.Instance.GamePlane.transform);
            shipInstance.gameObject.SetActive(true);
            shipInstance.transform.position = ship.ObjectStartPosition;
            shipInstance.transform.rotation = ship.ObjectStartRotation;

            var shipData = new ShipData(shipInstance.GetComponent<BaseMortalObjectController>(), shipInstance.transform.position);

            ships.Add(shipData);
        }

        return ships;
    }
    //todo handle activate/deactivate in one place
    private void ActivateShips(List<ShipData> shipObjects)
    {
        foreach (var ship in shipObjects)
        {
            if (!ship.mortalShip.Immortal && ship.mortalShip.CurrentLivesCount == 0) return;
            
            ship.mortalShip.transform.position = ship.startPosition;
            ship.mortalShip.gameObject.SetActive(true);
        }
    }

    private void SpawnPlayer()
    {
        _playerInstance = Instantiate(LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ObjectPrefab,
            GameLauncher.Instance.GamePlane.transform);
    }

    private void ActivatePlayer()
    {
        if (!_playerInstance) return;

        if (!LevelSettingsManager.Instance.CurrentLevel.ActivatePlayer)
        {
            _playerInstance.gameObject.SetActive(false);
            return;
        }

        _playerInstance.gameObject.SetActive(true);
        _playerInstance.transform.position = LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ObjectStartPosition;
        _playerInstance.transform.rotation = LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ObjectStartRotation;
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
        DestroyAllShips();
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

    public void DestroyAllShips()
    {
        foreach (var playerObject in _playerShips)
        {
            Destroy(playerObject.mortalShip.gameObject);
        }

        foreach (var playerObject in _enemyShips)
        {
            Destroy(playerObject.mortalShip.gameObject);
        }

        _playerShips.Clear();
        _enemyShips.Clear();
    }

    public void DeactivateAllShips()
    {
        foreach (var playerShip in _playerShips)
        {
            playerShip.mortalShip.gameObject.SetActive(false);
        }

        foreach (var enemyShip in _enemyShips)
        {
            enemyShip.mortalShip.gameObject.SetActive(false);
        }
    }

    public void PauseGameplay()
    {
        Time.timeScale = 0;
    }

    public void ResumeGameplay()
    {
        Time.timeScale = 1;
    }
}
