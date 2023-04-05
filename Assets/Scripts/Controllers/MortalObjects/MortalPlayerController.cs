using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTags.ASTEROID, GameObjectTags.BULLET };
    }

    protected override void OnCollisionWithEnemy(Collision2D collision)
    {
        DecrementLive();
        EventsManager.Instance.OnPlayerLoseLife(_livesCount);
    }

    private void OnEnable()
    {
        _livesCount = LevelSettingsManager.Instance.CurrentLevel.PlayerStartLivesCount;
        EventsManager.Instance.OnPlayerSpawned(_livesCount);
    }
}
