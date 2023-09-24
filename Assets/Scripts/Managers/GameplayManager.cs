using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayManager : BaseManager<GameplayManager>
{
    private BaseMortalObjectController _playerInstance;
    private List<GameObject> _playerShips = new List<GameObject>();
    private List<GameObject> _enemyShips = new List<GameObject>();

    #region States

    private StateMachine _gameplayStateMachine;

    public GameplayState GameplayState { get; private set; }
    public WinState WinState { get; private set; }
    public LoseState LoseState { get; private set; }
    public IdleState IdleState { get; private set; }
    public EndGameplayState EndGameplayState { get; private set; }

    #endregion

    public uint CurrentScore { get; private set; }
    public uint PlayerLivesCount => _playerInstance.LivesCount;
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
        EventsManager.Instance.AsteroidShotted += ObjectShotted;
        EventsManager.Instance.EnemyShotted += ObjectShotted;
        EventsManager.Instance.PlayerLoseLife += PlayerLoseLife;
    }

    private void PlayerLoseLife(uint lives)
    {
        if (PlayerLivesCount == 0)
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
        DestroyAllShips();
        AsteroidReleasingManager.Instance.StopReleasingAsteroidsCoroutine();
    }

    private void ObjectShotted(string tag)
    {
        if (!tag.Equals(GameObjectTagsConstants.PLAYER_BULLET)) return;

        IncrementScore();
    }

    public void StartCurrentLevel()
    {
        ResetScore();
        LevelSettingsManager.Instance.SetCurrentLevel();

        SpawnPlayer();

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
        _playerShips.AddRange(SpawnShips(LevelSettingsManager.Instance.CurrentLevel.PlayerObjects));
        _enemyShips.AddRange(SpawnShips(LevelSettingsManager.Instance.CurrentLevel.EnemyObjects));
        AsteroidReleasingManager.Instance.StartReleasingAsteroidCoroutine();
    }

    private List<GameObject> SpawnShips(List<ShipObject> shipObjects)
    {
        var ships = new List<GameObject>();
        foreach (var ship in shipObjects)
        {
            var shipInstance = Instantiate(ship.ObjectPrefab, GameLauncher.Instance.GamePlane.transform).GetComponent<BaseMortalObjectController>();
            shipInstance.gameObject.SetActive(true);
            shipInstance.transform.position = ship.ObjectStartPosition;
            shipInstance.transform.rotation = ship.ObjectStartRotation;
            shipInstance.SetLivesCount(ship.ShipLivesCount);

            ships.Add(shipInstance.gameObject);
        }

        return ships;
    }

    private void SpawnPlayer()
    {
        _playerInstance = Instantiate(LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ObjectPrefab,
            GameLauncher.Instance.GamePlane.transform).GetComponent<BaseMortalObjectController>();
        _playerInstance.SetLivesCount(LevelSettingsManager.Instance.CurrentLevel.MainPlayerObject.ShipLivesCount);
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
            Destroy(playerObject);
        }
        foreach (var playerObject in _enemyShips)
        {
            Destroy(playerObject);
        }
        _playerShips.Clear();
        _enemyShips.Clear();
    }

    public GameObject GetClosestEnemy(Vector3 position)
    {
        GameObject closest = null;
        var minDist = Mathf.Infinity;

        foreach (var enemy in _enemyShips)
        {
            if (!enemy.activeSelf) continue;

            var distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < minDist)
            {
                minDist = distance;
                closest = enemy;
            }
        }

        return closest;
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
