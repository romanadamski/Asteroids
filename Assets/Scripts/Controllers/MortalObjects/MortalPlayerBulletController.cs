using UnityEngine;

public class MortalPlayerBulletController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID, GameObjectTagsConstants.ENEMY_SHIP };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collider)
    {
        ShootingManager.Instance.ObjectPoolingController.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
        EventsManager.Instance.OnEnemyShotted(collider.transform.tag);
    }
}
