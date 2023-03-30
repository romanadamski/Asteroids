using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        //todo where to implement actions on player death
        Managers.EventsManager.PlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath(uint livesCount)
    {
    }
}
