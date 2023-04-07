using UnityEngine;

public class BaseBulletMovementController : BaseMovementController
{
    protected override void MoveObject(Vector3 direction, float speedMultiplier)
    {
        _rigidbody2D.velocity = direction * GameSettingsManager.Instance.Settings.BaseBulletMovementSpeed * speedMultiplier;
    }
}
