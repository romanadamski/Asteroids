using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseCollisionController))]
public abstract class BaseMortalObjectController : MonoBehaviour
{
    private string[] _enemyObjectsTags;
    private BaseCollisionController _collideController;

    protected bool _enemyCollideExited = true;
    protected virtual void OnCollisionWithEnemyEnter(Collision2D collision) { }
    protected virtual void OnCollisionWithEnemyExit(Collision2D collision) { }
    protected virtual string[] GetEnemies() { return new string[] { }; }

    protected uint _livesCount;
    public uint LivesCount => _livesCount;

    private void Awake()
    {
        _enemyObjectsTags = GetEnemies();
        _collideController = GetComponent<BaseCollisionController>();
        _collideController.CollisionEnter += CollisionEnter;
        _collideController.CollisionExit += CollisionExit;
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
        if (_livesCount > 0)
        {
            _livesCount--;
        }
    }
}
