using System.Linq;
using UnityEngine;

public abstract class BaseMortalObjectController : MonoBehaviour
{
    [SerializeField]
    protected uint livesCount;
    public uint LivesCount => livesCount;

    [SerializeField]
    protected bool immortal;
    public bool Immortal => immortal;

    private string[] _enemyObjectsTags;

    protected bool _enemyCollideExited = true;
    protected bool _enemyTriggerExited = true;

    protected virtual void OnCollisionWithEnemyEnter(Collision2D collision) { }
    protected virtual void OnCollisionWithEnemyExit(Collision2D collision) { }
    protected virtual void OnTriggerWithEnemyEnter(Collider2D collider) { }
    protected virtual void OnTriggerWithEnemyExit(Collider2D collider) { }
    protected virtual string[] GetEnemies() { return new string[] { }; }

    public uint CurrentLivesCount { get; protected set; }

    private void Awake()
    {
        _enemyObjectsTags = GetEnemies();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_enemyObjectsTags.Contains(collision.transform.tag)) return;
        if (!gameObject.activeSelf) return;
        if (!_enemyCollideExited) return;
        
        _enemyCollideExited = false;
        DecrementLive();
        OnCollisionWithEnemyEnter(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!_enemyObjectsTags.Contains(collision.transform.tag)) return;

        _enemyCollideExited = true;
        OnCollisionWithEnemyExit(collision);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_enemyObjectsTags.Contains(collider.tag)) return;
        if (!gameObject.activeSelf) return;
        if (!_enemyTriggerExited) return;
        
        _enemyTriggerExited = false;

        DecrementLive();
        OnTriggerWithEnemyEnter(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!_enemyObjectsTags.Contains(collider.tag)) return;

        _enemyTriggerExited = true;
        OnTriggerWithEnemyExit(collider);
    }

    protected void DecrementLive()
    {
        if (!immortal && CurrentLivesCount > 0)
        {
            CurrentLivesCount--;
        }
    }
}
