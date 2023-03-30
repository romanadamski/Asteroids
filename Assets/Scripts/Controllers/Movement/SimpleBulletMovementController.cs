using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SimpleBulletMovementController : BaseMovementController
{
    public override void OnOutsideScreen()
    {
        ReturnToPool();
        _isReleased = false;
    }

    protected override void MoveObject()
    {
        _rigidbody2D.velocity = transform.up * Managers.SettingsManager.Settings.BaseBulletMovementSpeed * _speedMultiplier;
    }

    private void ReturnToPool()
    {
        Managers.ObjectPoolingManager.ReturnToPool(gameObject);
    }
}
