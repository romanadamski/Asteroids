using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    [SerializeField]
    private bool mainPlayer;
    [SerializeField]
    private float respawnDelay;

    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID, GameObjectTagsConstants.ENEMY_SHIP, GameObjectTagsConstants.ENEMY_BULLET };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collision)
    {
        if (mainPlayer)
        {
            EventsManager.Instance.OnPlayerLoseLife(CurrentLivesCount);
        }
        else
        {
            gameObject.SetActive(false);
            if (CurrentLivesCount > 0)
            {
                Invoke(nameof(Respawn), respawnDelay);
            }
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        CurrentLivesCount = livesCount;
        if (mainPlayer)
        {
            EventsManager.Instance.OnPlayerSpawned(CurrentLivesCount);
        }
    }
}
