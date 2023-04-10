using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID };
    }

    protected override void OnCollisionWithEnemyEnter(Collision2D collision)
    {
        DecrementLive();
        GameplayManager.Instance.SetDeathState();
        EventsManager.Instance.OnPlayerLoseLife(LivesCount);
    }

    private void Start()
    {
        LivesCount = LevelSettingsManager.Instance.CurrentLevel.PlayerStartLivesCount;
        EventsManager.Instance.OnPlayerSpawned(LivesCount);
    }
}
