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

    private void Awake()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerLoseLife += PlayerLoseLife;
        EventsManager.Instance.PlayerSpawned += PlayerSpawned;
    }

    private void UnsubscribeFromEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.PlayerLoseLife -= PlayerLoseLife;
        EventsManager.Instance.PlayerSpawned -= PlayerSpawned;
    }

    private void PlayerSpawned(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void PlayerLoseLife(uint lives)
    {
        SetLivesCounter(lives);
    }

    public void SetLivesCounter(uint lives)
    {
        livesCounter.text = lives.ToString();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
