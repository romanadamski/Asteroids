using UnityEngine;

public abstract class BaseMortalObjectController : MonoBehaviour
{
    protected bool _enemyCollideExited = true;
    protected bool _enemyTriggerExited = true;

    protected virtual void OnCollisionWithEnemyEnter(Collision2D collision) { }
    protected virtual void OnCollisionWithEnemyExit(Collision2D collision) { }
    protected virtual void OnTriggerWithEnemyEnter(Collider2D collider) { }
    protected virtual void OnTriggerWithEnemyExit(Collider2D collider) { }

    public uint LivesCount { get; protected set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameObject.activeSelf) return;
        if (!_enemyCollideExited) return;
        
        _enemyCollideExited = false;
        OnCollisionWithEnemyEnter(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _enemyCollideExited = true;
        OnCollisionWithEnemyExit(collision);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!gameObject.activeSelf) return;
        if (!_enemyTriggerExited) return;
        
        _enemyTriggerExited = false;
        OnTriggerWithEnemyEnter(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
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
}
