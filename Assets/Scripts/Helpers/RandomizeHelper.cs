using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeHelper
{
    private const float DIRECTION_RANDOMIZE_RANGE = 0.8f;

    public static Vector2 GetRandomDirectionDependsOnPosition(Transform asteroid)
    {
        var direction = ScreenManager.Instance.GetObjectPositionRelativeToScreen(asteroid);
        var randomX = 0.0f;
        var randomY = 0.0f;

        if (direction.Equals(ScreenDirectionEnum.SCREEN))
        {
            randomX = Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
            randomY = Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
        }
        else
        {
            if (direction.HasFlag(ScreenDirectionEnum.DOWN))
            {
                randomX += Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
                randomY += 1;
            }
            else if (direction.HasFlag(ScreenDirectionEnum.UP))
            {
                randomX += Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
                randomY += -1;
            }

            if (direction.HasFlag(ScreenDirectionEnum.LEFT))
            {
                randomX += 1;
                randomY += Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
            }
            else if (direction.HasFlag(ScreenDirectionEnum.RIGHT))
            {
                randomX += -1;
                randomY += Random.Range(-DIRECTION_RANDOMIZE_RANGE, DIRECTION_RANDOMIZE_RANGE);
            }
        }

        return new Vector2(randomX, randomY).normalized;
    }
}
