using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseCollisionController : MonoBehaviour
{
    public event Action<Collision2D> CollisionEnter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollide(collision);
    }

    private void OnCollide(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }
}
