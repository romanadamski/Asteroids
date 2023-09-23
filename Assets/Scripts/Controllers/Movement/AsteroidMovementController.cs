using System;
using System.Collections;
using UnityEngine;

public class AsteroidMovementController : BaseMovementController
{
    private bool _firstScreenApperance;
    private DateTime _outsideScreenStartTime;
    private bool _isOutsideScreenTimeSet;
    private Coroutine _rotationCoroutine;
    private float _maxOutsideScreenTime = 5;

    private void RotateTowardsVelocity(bool smooth = true)
    {
        if (smooth)
        {
            StopRotationCoroutine();
            if (gameObject.activeSelf)
            {
                _rotationCoroutine = StartCoroutine(SmoothRotateTowardsVelocity());
            }
        }
        else
        {
            transform.rotation = GetVelocityRotation();
        }
    }

    private IEnumerator SmoothRotateTowardsVelocity()
    {
        var targetAngle = GetVelocityRotation();
        while (transform.rotation != targetAngle)
        {
            transform.rotation = Quaternion.Lerp(
                            transform.rotation,
                            targetAngle,
                            Time.deltaTime);

            yield return null;
        }
    }

    private void StopRotationCoroutine()
    {
        if (_rotationCoroutine != null)
        {
            StopCoroutine(_rotationCoroutine);
            _rotationCoroutine = null;
        }
    }

    private Quaternion GetVelocityRotation()
    {
        float angle = (Mathf.Atan2(-_rigidbody2D.velocity.x, _rigidbody2D.velocity.y) * Mathf.Rad2Deg) - 90;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

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
            DeactivateMovingObject();
        }
    }

    protected override void DeactivateMovingObject()
    {
        _firstScreenApperance = false;
        _isOutsideScreenTimeSet = false;
        base.DeactivateMovingObject();
    }

    protected override void OnInsideScreen()
    {
        if (!_firstScreenApperance)
        {
            _firstScreenApperance = true;
        }
    }

    protected override void MoveObject(Vector3 direction)
    {
        _rigidbody2D.velocity = direction * GameSettingsManager.Instance.Settings.BaseAsteroidMovementSpeed * _speedMultiplier;
        RotateTowardsVelocity(smooth: false);
    }
}
