using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovementController : BaseMovementController
{
    private bool _firstScreenApperance;
    private DateTime _outsideScreenStartTime;
    private bool _isOutsideScreenTimeSet;

    [Tooltip("Time in seconds")]
    [SerializeField]
    private float _maxOutsideScreenTime;

    public override void OnOutsideScreen()
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
            ReturnToPool();
        }
    }

    public override void OnInsideScreen()
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
