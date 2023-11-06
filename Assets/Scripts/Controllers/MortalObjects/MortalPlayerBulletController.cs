using UnityEngine;

public class MortalPlayerBulletController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID, GameObjectTagsConstants.ENEMY_SHIP };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collider)
    {
        //ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }
}
