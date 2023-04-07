﻿public class BaseBulletMovementController : BaseMovementController
{
    protected override void MoveObject()
    {
        _rigidbody2D.velocity = transform.up * GameSettingsManager.Instance.Settings.BaseBulletMovementSpeed * _speedMultiplier;
    }
}
