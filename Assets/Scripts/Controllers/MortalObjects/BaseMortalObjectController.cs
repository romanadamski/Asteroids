using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseCollisionController))]
public abstract class BaseMortalObjectController : MonoBehaviour
{
    private string[] _enemyObjectsTags;
    private BaseCollisionController _collisionController;

    protected bool _enemyCollideExited = true;
    protected virtual void OnCollisionWithEnemyEnter(Collision2D collision) { }
    protected virtual void OnCollisionWithEnemyExit(Collision2D collision) { }
    protected virtual string[] GetEnemies() { return new string[] { }; }

    public uint LivesCount { get; protected set; }

    private void Awake()
    {
        _enemyObjectsTags = GetEnemies();
        _collisionController = GetComponent<BaseCollisionController>();
        _collisionController.CollisionEnter += CollisionEnter;
        _collisionController.CollisionExit += CollisionExit;
    }

    private void CollisionEnter(Collision2D collision)
    {
        if (_enemyCollideExited && _enemyObjectsTags.Contains(collision.transform.tag))
        {
            _enemyCollideExited = false;
            OnCollisionWithEnemyEnter(collision);
        }
    }

    private void CollisionExit(Collision2D collision)
    {
        if (_enemyObjectsTags.Contains(collision.transform.tag))
        {
            _enemyCollideExited = true;
            OnCollisionWithEnemyExit(collision);
        }
    }

    protected void DecrementLive()
    {
        if (LivesCount > 0)
        {
            LivesCount--;
        }
    }

    private void OnDestroy()
    {
        _collisionController.CollisionEnter -= CollisionEnter;
        _collisionController.CollisionExit -= CollisionExit;
    }
}
