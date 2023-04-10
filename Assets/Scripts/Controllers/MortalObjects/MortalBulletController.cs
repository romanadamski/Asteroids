using UnityEngine;

public class MortalBulletController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID,GameObjectTagsConstants.PLAYER };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collider)
    {
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }
}
