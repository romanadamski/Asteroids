using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleBullet : Bullet
{
    public override void OnScreenEdgesCrossed()
    {
        ReturnToPool();
        _isReleased = false;
    }

    private void ReturnToPool()
    {
        Managers.ObjectPoolingManager.ReturnToPool("SimpleBullet", gameObject);
    }
}
