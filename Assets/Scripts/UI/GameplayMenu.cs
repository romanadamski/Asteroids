using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameplayMenu : BaseMenu
{
    [SerializeField]
    TextMeshProUGUI livesCounter;
    [SerializeField]
    TextMeshProUGUI levelNumberCounter;
    [SerializeField]
    TextMeshProUGUI scoreCounter;

    private void Awake()
    {
        SubscribeToEvents();
    }

    private void OnEnable()
    {
        SetScore(0);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerLoseLife += OnPlayerLoseLife;
        EventsManager.Instance.PlayerSpawned += OnPlayerSpawned;
        EventsManager.Instance.GameplayStarted += OnGameplayStarted;
        EventsManager.Instance.ScoreUpdated += OnScoreUpdated;
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
        EventsManager.Instance.GameplayStarted -= OnGameplayStarted;
        EventsManager.Instance.ScoreUpdated -= OnScoreUpdated;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
