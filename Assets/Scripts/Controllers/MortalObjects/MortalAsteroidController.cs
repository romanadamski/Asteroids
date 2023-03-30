﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MortalAsteroidController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { "Bullet" };
    }

    protected override void OnCollisionWithEnemy(Collision2D collision)
    {
        _livesCount--;
        Managers.ObjectPoolingManager.ReturnToPool(gameObject);
        Managers.EventsManager.OnAsteroidShotted(_livesCount);
    }
}