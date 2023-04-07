using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameplayMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI livesCounter;
    [SerializeField]
    private TextMeshProUGUI levelNumberCounter;
    [SerializeField]
    private TextMeshProUGUI scoreCounter;
    [SerializeField]
    private Button menuButton;

    private void Awake()
    {
        SubscribeToEvents();
        menuButton.onClick.AddListener(OnGoToMainMenuClick);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerLoseLife += OnPlayerLoseLife;
        EventsManager.Instance.PlayerSpawned += OnPlayerSpawned;
        EventsManager.Instance.LevelStarted += OnGameplayStarted;
        EventsManager.Instance.ScoreUpdated += OnScoreUpdated;
    }

    private void OnGoToMainMenuClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void OnGameplayStarted(uint levelNumber)
    {
        SetLevelNumber(levelNumber);
    }

    private void OnPlayerSpawned(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void OnPlayerLoseLife(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void OnScoreUpdated(uint score)
    {
        SetScore(score);
    }

    private void SetLivesCounter(uint lives)
    {
        livesCounter.text = lives.ToString();
    }

    private void SetLevelNumber(uint levelNumber)
    {
        levelNumberCounter.text = levelNumber.ToString();
    }

    private void SetScore(uint score)
    {
        scoreCounter.text = score.ToString();
    }

    private void UnsubscribeFromEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.PlayerLoseLife -= OnPlayerLoseLife;
        EventsManager.Instance.PlayerSpawned -= OnPlayerSpawned;
        EventsManager.Instance.LevelStarted -= OnGameplayStarted;
        EventsManager.Instance.ScoreUpdated -= OnScoreUpdated;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
