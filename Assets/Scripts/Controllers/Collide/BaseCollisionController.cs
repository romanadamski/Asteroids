using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseCollisionController : MonoBehaviour
{
    public event Action<Collision2D> CollisionEnter;
    public event Action<Collision2D> CollisionExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollideStart(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollideEnd(collision);
    }

    private void OnCollideStart(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }

    private void OnCollideEnd(Collision2D collision)
    {
        CollisionExit?.Invoke(collision);
    }
}
