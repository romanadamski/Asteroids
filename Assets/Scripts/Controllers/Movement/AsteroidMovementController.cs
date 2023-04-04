using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovementController : BaseMovementController
{
    private bool _firstScreenApperance;
    private DateTime _outsideScreenStartTime;
    private bool _isOutsideScreenTimeSet;

    [SerializeField]
    private float _maxOutsideScreenTime;

    public override void OnOutsideScreen()
    {
        if (!_isOutsideScreenTimeSet)
        {
            _outsideScreenStartTime = DateTime.Now;
            _isOutsideScreenTimeSet = true;
        }

        if ((DateTime.Now - _outsideScreenStartTime).TotalSeconds > _maxOutsideScreenTime)
        {
            ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
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
        _rigidbody2D.velocity = transform.up * SettingsManager.Instance.Settings.BaseAsteroidMovementSpeed * _speedMultiplier;
    }
}
