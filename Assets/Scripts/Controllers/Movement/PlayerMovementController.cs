using UnityEngine;

public class PlayerMovementController : BaseMovementController
{
    private Vector2 MovementAxis => new Vector2(CalculateAxis(MovementTrigger.XAxis), CalculateAxis(MovementTrigger.YAxis));

    private float CalculateAxis(float axis)
    {
        return axis * GameSettingsManager.Instance.Settings.PlayerMovementSpeed * (speedMultiplier / 5);
    }

    protected override void HandleMovement()
    {
        _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, MovementAxis, Time.deltaTime * GameSettingsManager.Instance.Settings.PlayerMovementPrecision);
        Rotate();
    }

    protected override void OnOutsideScreen()
    {
        ScreenManager.Instance.HandleScreenEdgeCrossing(transform);
    }

    private void Rotate()
    {
        if (_rigidbody2D.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(-_rigidbody2D.velocity.x, _rigidbody2D.velocity.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward),
                Time.deltaTime * GameSettingsManager.Instance.Settings.PlayerRotationSpeed);
        }
    }

    private void OnDisable()
    {
        StopMovement();
    }
}
