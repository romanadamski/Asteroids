﻿using UnityEngine;

public class MortalEnemyBulletController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID, GameObjectTagsConstants.PLAYER_SHIP };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collider)
    {
        ShootingManager.Instance.ObjectPoolingController.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }
}
