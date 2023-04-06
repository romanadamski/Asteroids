using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventsManager : BaseManager<EventsManager>
{
    public event Action<uint> PlayerLoseLife;
    public void OnPlayerLoseLife(uint livesCount)
    {
        PlayerLoseLife?.Invoke(livesCount);
    }

    public event Action<uint> PlayerSpawned;
    public void OnPlayerSpawned(uint livesCount)
    {
        PlayerSpawned?.Invoke(livesCount);
    }
    
    public event Action AsteroidShotted;
    public void OnAsteroidShotted()
    {
        AsteroidShotted?.Invoke();
    }
    
    public event Action<uint> GameplayStarted;
    public void OnGameplayStarted(uint levelNumber)
    {
        GameplayStarted?.Invoke(levelNumber);
    }


    public event Action<uint> ScoreUpdated;
    public void OnScoreUpdated(uint score)
    {
        ScoreUpdated?.Invoke(score);
    }

}
