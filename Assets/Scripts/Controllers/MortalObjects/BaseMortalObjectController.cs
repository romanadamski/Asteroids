using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class BaseMortalObjectController : MonoBehaviour
{
    [SerializeField]
    protected uint _livesCount;
    private string[] _enemyObjectsTags;
    
    private void Awake()
    {
        _enemyObjectsTags = GetEnemies();
    }

    protected abstract void OnCollisionWithEnemy(Collision2D collision);
    protected abstract string[] GetEnemies();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var message = $"{gameObject.name}: {transform.tag} collided with {collision.gameObject.name} {collision.transform.tag}";
        if (_enemyObjectsTags.Contains(collision.transform.tag))
        {
            OnCollisionWithEnemy(collision);
            message += " ENEMY!";
        }
        Debug.Log(message);
    }
}
