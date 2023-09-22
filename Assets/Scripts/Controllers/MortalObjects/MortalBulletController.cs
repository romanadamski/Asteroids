using UnityEngine;

public class MortalBulletController : BaseMortalObjectController
{
    protected override void OnTriggerWithEnemyEnter(Collider2D collider)
    {
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }
}
