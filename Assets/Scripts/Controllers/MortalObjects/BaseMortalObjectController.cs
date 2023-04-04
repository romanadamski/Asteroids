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

    protected abstract void OnCollisionWithEnemy(Collision2D collision);
    protected abstract string[] GetEnemies();

    private void Awake()
    {
        _enemyObjectsTags = GetEnemies();
        _collideController = GetComponent<BaseCollisionController>();
        _collideController.CollisionEnter += OnCollision;
    }

    private void OnCollision(Collision2D collision)
    {
        var message = $"{gameObject.name}: {transform.tag} collided with {collision.gameObject.name} {collision.transform.tag}";
        if (_enemyObjectsTags.Contains(collision.transform.tag))
        {
            OnCollisionWithEnemy(collision);
            message += " ENEMY!";
        }
        Debug.Log(message);
    }

    protected void DecrementLive()
    {
        if (_livesCount > 0)
        {
            _livesCount--;
        }
    }
}
