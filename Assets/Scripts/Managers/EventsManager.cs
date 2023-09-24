using System;

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
    
    public event Action<string> AsteroidShotted;
    public void OnAsteroidShotted(string tag)
    {
        AsteroidShotted?.Invoke(tag);
    }
    
    public event Action<string> EnemyShotted;
    public void OnEnemyShotted(string tag)
    {
        EnemyShotted?.Invoke(tag);
    }
    
    public event Action<LevelSettingsSO> LevelStarted;
    public void OnLevelStarted(LevelSettingsSO levelNumber)
    {
        LevelStarted?.Invoke(levelNumber);
    }
    
    public event Action GameplayStarted;
    public void OnGameplayStarted()
    {
        GameplayStarted?.Invoke();
    }

    public event Action<uint> ScoreUpdated;
    public void OnScoreUpdated(uint score)
    {
        ScoreUpdated?.Invoke(score);
    }

    public event Action BulletFired;
    public void OnBulletFired()
    {
        BulletFired?.Invoke();
    }
}
