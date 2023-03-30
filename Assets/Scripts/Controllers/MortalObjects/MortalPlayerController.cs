using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { "Bullet", "Asteroid" };
    }

    protected override void OnCollisionWithEnemy(Collision2D collision)
    {
        _livesCount--;
        Managers.EventsManager.OnPlayerDeath(_livesCount);
    }
}
