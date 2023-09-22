using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    protected override void OnTriggerWithEnemyEnter(Collider2D collision)
    {
        DecrementLive();
        EventsManager.Instance.OnPlayerLoseLife(LivesCount);
    }

    private void Start()
    {
        LivesCount = LevelSettingsManager.Instance.CurrentLevel.PlayerStartLivesCount;
        EventsManager.Instance.OnPlayerSpawned(LivesCount);
    }
}
