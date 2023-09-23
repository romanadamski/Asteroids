using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID, GameObjectTagsConstants.ENEMY_SHIP, GameObjectTagsConstants.ENEMY_BULLET };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collision)
    {
        DecrementLive();
        EventsManager.Instance.OnPlayerLoseLife(LivesCount);
    }

    private void Start()
    {
        EventsManager.Instance.OnPlayerSpawned(LivesCount);
    }
}
