using UnityEngine;

public class AsteroidsRandomizeHelper : RandomizeHelper
{
    public static float GetRandomAsteroidSpeed()
    {
        return Random.Range(
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsSpeedRange.Item1,
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsSpeedRange.Item2);
    }

    public static float GetRandomAsteroidFrequency()
    {
        return Random.Range(
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsReleasingFrequency.Item1,
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsReleasingFrequency.Item2);
    }

    public static Vector2 GetRandomAsteroidPositionOutsideScreen()
    {
        var values = System.Enum.GetValues(typeof(ScreenDirectionEnum));
        var randomDirection = (ScreenDirectionEnum)values.GetValue(Random.Range(1, values.Length));

        var randomX = 0.0f;
        var randomY = 0.0f;

        switch (randomDirection)
        {
            case ScreenDirectionEnum.DOWN:
                randomX = GetRandomXScreenPosition();
                randomY = -1;
                break;
            case ScreenDirectionEnum.UP:
                randomX = GetRandomXScreenPosition();
                randomY = Screen.height + 1;
                break;
            case ScreenDirectionEnum.RIGHT:
                randomX = Screen.width + 1;
                randomY = GetRandomYScreenPosition();
                break;
            case ScreenDirectionEnum.LEFT:
                randomX = -1;
                randomY = GetRandomYScreenPosition();
                break;
        }

        return Camera.main.ScreenToWorldPoint(new Vector2(randomX, randomY));
    }

    private static float GetRandomXScreenPosition()
    {
        return Random.Range(-1.0f, Screen.width + 1);
    }

    private static float GetRandomYScreenPosition()
    {
        return Random.Range(-1.0f, Screen.height + 1);
    }
}
