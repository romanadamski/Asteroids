using System;
using UnityEngine;

public class AsteroidMovementController : BaseMovementController
{
    private bool _firstScreenApperance;
    private DateTime _outsideScreenStartTime;
    private bool _isOutsideScreenTimeSet;

    [Tooltip("Time in seconds")]
    [SerializeField]
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
            DeactivaleMovingObject();
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
        //todo moving asteroid
        _rigidbody2D.velocity = transform.up * GameSettingsManager.Instance.Settings.BaseAsteroidMovementSpeed * _speedMultiplier;
    }
}
