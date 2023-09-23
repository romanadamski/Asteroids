using UnityEngine;

public class BaseBulletMovementController : BaseMovementController
{
    protected override void MoveObject()
    {
        _rigidbody2D.velocity = MovementTrigger.MovementDirection * GameSettingsManager.Instance.Settings.BaseBulletMovementSpeed * speedMultiplier;
    }
}
