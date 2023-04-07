using UnityEngine;

public class AsteroidsRandomizeHelper
{
    public float GetRandomAsteroidSpeed()
    {
        return Random.Range(
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsSpeedRange.Item1,
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsSpeedRange.Item2);
    }

    public float GetRandomAsteroidFrequency()
    {
        return Random.Range(
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsReleasingFrequency.Item1,
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsReleasingFrequency.Item2);
    }

    public Vector2 GetRandomAsteroidDirection()
    {
        var randomX = Random.Range(-1.0f, 1.0f);
        var randomY = Random.Range(-1.0f, 1.0f);
        return new Vector2(randomX, randomY);
    }
}
