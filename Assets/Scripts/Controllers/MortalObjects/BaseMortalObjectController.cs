﻿using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseCollisionController))]
public abstract class BaseMortalObjectController : MonoBehaviour
{
    private BaseCollisionController _collisionController;

    protected bool _enemyCollideExited = true;
    protected bool _enemyTriggerExited = true;

    protected virtual void OnCollisionWithEnemyEnter(Collision2D collision) { }
    protected virtual void OnCollisionWithEnemyExit(Collision2D collision) { }
    protected virtual void OnTriggerWithEnemyEnter(Collider2D collider) { }
    protected virtual void OnTriggerWithEnemyExit(Collider2D collider) { }

    public uint LivesCount { get; protected set; }

    private void Awake()
    {
        _collisionController = GetComponent<BaseCollisionController>();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _collisionController.CollisionEnter += CollisionEnter;
        _collisionController.CollisionExit += CollisionExit;
        _collisionController.TriggerEnter += TriggerEnter;
        _collisionController.TriggerExit += TriggerExit;
    }

    private void CollisionEnter(Collision2D collision)
    {
        if (_enemyCollideExited)
        {
            _enemyCollideExited = false;
            OnCollisionWithEnemyEnter(collision);
        }
    }

    private void CollisionExit(Collision2D collision)
    {
        _enemyCollideExited = true;
        OnCollisionWithEnemyExit(collision);
    }

    private void TriggerEnter(Collider2D collider)
    {
        if (_enemyTriggerExited)
        {
            _enemyTriggerExited = false;
            OnTriggerWithEnemyEnter(collider);
        }
    }

    private void TriggerExit(Collider2D collider)
    {
        _enemyTriggerExited = true;
        OnTriggerWithEnemyExit(collider);
    }

    protected void DecrementLive()
    {
        if (LivesCount > 0)
        {
            LivesCount--;
        }
    }

    private void UnsubscribeFromEvents()
    {
        _collisionController.CollisionEnter -= CollisionEnter;
        _collisionController.CollisionExit -= CollisionExit;
        _collisionController.TriggerEnter -= TriggerEnter;
        _collisionController.TriggerExit -= TriggerExit;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
