using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID, GameObjectTagsConstants.BULLET };
    }

    protected override void OnCollisionWithEnemyEnter(Collision2D collision)
    {
        DecrementLive();
        GameplayManager.Instance.SetDeathState();
        EventsManager.Instance.OnPlayerLoseLife(_livesCount);
    }

    private void Start()
    {
        _livesCount = LevelSettingsManager.Instance.CurrentLevel.PlayerStartLivesCount;
        EventsManager.Instance.OnPlayerSpawned(_livesCount);
    }
}
