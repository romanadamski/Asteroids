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

    public void SpawnPlayer()
    {
        if (_playerInstance == null)
        {
            _playerInstance = Instantiate(playerPrefab, GameLauncher.Instance.GamePlane.transform);
        }
        _playerInstance.SetActive(true);
        _playerInstance.transform.position = Vector3.zero;
        _playerInstance.transform.rotation = Quaternion.Euler(Vector3.zero);//todo in settings/level config set default position and rotation
    }

    public void DespawnPlayer()
    {
        if (_playerInstance == null) return;
        
        _playerInstance.SetActive(false);
    }

    public void ReturnAllBullets()
    {
        ObjectPoolingManager.Instance.ReturnAllToPool(typeof(CrossingEdgesBulletMovementController).Name);//todo take bullets from somewhere
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
