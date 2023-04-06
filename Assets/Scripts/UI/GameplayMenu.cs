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
    TextMeshProUGUI levelNumber;

    private void Awake()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerLoseLife += PlayerLoseLife;
        EventsManager.Instance.PlayerSpawned += PlayerSpawned;
        EventsManager.Instance.GameplayStarted += GameplayStarted;
    }

    private void GameplayStarted(uint levelNumber)
    {
        SetLevelNumber(levelNumber);
    }

    private void SetLevelNumber(uint level)
    {
        levelNumber.text = level.ToString();
    }

    private void PlayerSpawned(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void PlayerLoseLife(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void SetLivesCounter(uint lives)
    {
        livesCounter.text = lives.ToString();
    }

    private void UnsubscribeFromEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.PlayerLoseLife -= PlayerLoseLife;
        EventsManager.Instance.PlayerSpawned -= PlayerSpawned;
        EventsManager.Instance.GameplayStarted -= GameplayStarted;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
