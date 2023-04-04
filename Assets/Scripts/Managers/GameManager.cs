using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    private void Start()
    {
        //todo where to implement actions on player death
        EventsManager.Instance.PlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath(uint livesCount)
    {
    }
}
