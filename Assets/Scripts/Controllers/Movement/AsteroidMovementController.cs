using System;
using System.Collections;
using UnityEngine;

public class AsteroidMovementController : BaseMovementController
{
    private bool _firstScreenApperance;
    private DateTime _outsideScreenStartTime;
    private bool _isOutsideScreenTimeSet;
    private float _maxOutsideScreenTime = 5;

    protected override void OnOutsideScreen()
    {
        //wait till object appears on screen
        if (!_firstScreenApperance) return;

        if (!_isOutsideScreenTimeSet)
        {
            _outsideScreenStartTime = DateTime.Now;
            _isOutsideScreenTimeSet = true;
        }

        if ((DateTime.Now - _outsideScreenStartTime).TotalSeconds > _maxOutsideScreenTime)
        {
            _firstScreenApperance = false;
            _isOutsideScreenTimeSet = false;
            DeactivateMovingObject();
        }
    }

    protected override void OnInsideScreen()
    {
        if (!_firstScreenApperance)
        {
            _firstScreenApperance = true;
        }
    }

    protected override void MoveObject()
    {
        _rigidbody2D.velocity = MovementTrigger.MovementDirection * GameSettingsManager.Instance.Settings.BaseAsteroidMovementSpeed * AsteroidsRandomizeHelper.GetRandomAsteroidSpeed();
        RotateTowardsVelocity();
    }

    private void RotateTowardsVelocity()
    {
        transform.rotation = GetVelocityRotation();
    }

    private Quaternion GetVelocityRotation()
    {
        float angle = (Mathf.Atan2(-_rigidbody2D.velocity.x, _rigidbody2D.velocity.y) * Mathf.Rad2Deg) - 90;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
