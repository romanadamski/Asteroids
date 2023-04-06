using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MortalAsteroidController : BaseMortalObjectController
{
    [SerializeField]
    private uint pieceCount;
    [SerializeField]
    private string pieceType;
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.BULLET };
    }

    protected override void OnCollisionWithEnemyEnter(Collision2D collision)
    {
        DecrementLive();
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
        for (int i = 0; i < pieceCount; i++)
        {
            var asteroid = ObjectPoolingManager.Instance.GetFromPool(pieceType);
            AsteroidsManager.Instance.ReleaseAsteroid(asteroid.gameObject);
        }

        EventsManager.Instance.OnAsteroidShotted();
    }
}
