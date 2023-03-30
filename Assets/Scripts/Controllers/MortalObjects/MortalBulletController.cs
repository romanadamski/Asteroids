﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MortalBulletController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { "Asteroid", "Player" };
    }

    protected override void OnCollisionWithEnemy(Collision2D collision)
    {
        Managers.ObjectPoolingManager.ReturnToPool(gameObject);
    }
}