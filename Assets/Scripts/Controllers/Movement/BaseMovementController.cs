using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public abstract class BaseMovementController : MonoBehaviour
{
    [SerializeField]
    protected float _speedMultiplier = 1;

    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;

    protected abstract void MoveObject();
    public virtual void OnOutsideScreen() { }
    public virtual void OnInsideScreen() { }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (!GeometryUtility.TestPlanesAABB(planes, _spriteRenderer.bounds))
        {
            OnOutsideScreen();
        }
        else
        {
            OnInsideScreen();
        }
    }

    public void Release()
    {
        MoveObject();
    }

    protected void ReturnToPool()
    {
        StopMovement();
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }

    private void StopMovement()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
    }
}
