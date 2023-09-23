
public class BaseBulletMovementController : BaseMovementController
{
    protected override void HandleMovement()
    {
        _rigidbody2D.velocity = MovementTrigger.MovementDirection * GameSettingsManager.Instance.Settings.BaseBulletMovementSpeed * speedMultiplier;
    }
}
