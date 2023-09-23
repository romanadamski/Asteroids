using UnityEngine;

public class MortalEnemyController : BaseMortalObjectController
{
    [SerializeField]
    private float respawnDelay;

    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID, GameObjectTagsConstants.PLAYER_BULLET };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collision)
    {
        EventsManager.Instance.OnEnemyShotted(collision.transform.tag);

        gameObject.SetActive(false);
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
    }
}
