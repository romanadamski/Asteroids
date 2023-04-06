using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(BaseCollisionController))]
public abstract class BaseMortalObjectController : MonoBehaviour
{
    protected uint _livesCount;
    public uint LivesCount => _livesCount;

    private string[] _enemyObjectsTags;
    private BaseCollisionController _collideController;

    protected bool _collideExit = true;
    protected virtual void OnCollisionWithEnemyEnter(Collision2D collision) { }
    protected virtual void OnCollisionWithEnemyExit(Collision2D collision) { }
    protected abstract string[] GetEnemies();

    private void Awake()
    {
        _enemyObjectsTags = GetEnemies();
        _collideController = GetComponent<BaseCollisionController>();
        _collideController.CollisionEnter += CollisionEnter;
        _collideController.CollisionExit += CollisionExit;
    }

    private void CollisionEnter(Collision2D collision)
    {
        if (!_collideExit) return;
        Debug.Log($"Entering collision: {name}");

        _collideExit = false;
        
        if (_enemyObjectsTags.Contains(collision.transform.tag))
        {
            OnCollisionWithEnemyEnter(collision);
        }
    }

    private void CollisionExit(Collision2D collision)
    {
        Debug.Log($"Exiting collision: {name}");
        _collideExit = true;
        OnCollisionWithEnemyExit(collision);
    }

    protected void DecrementLive()
    {
        if (_livesCount > 0)
        {
            _livesCount--;
        }
    }
}
