using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BaseBulletMovementController : BaseMovementController
{
    protected override void MoveObject()
    {
        _rigidbody2D.velocity = transform.up * Managers.SettingsManager.Settings.BaseBulletMovementSpeed * _speedMultiplier;
    }

    protected void ReturnToPool()
    {
        Managers.ObjectPoolingManager.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }

    private void OnDisable()
    {
        _isReleased = false;
    }
}
