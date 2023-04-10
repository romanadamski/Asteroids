using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public abstract class BaseMovementController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    protected Rigidbody2D _rigidbody2D;
    protected float _speedMultiplier;

    protected abstract void MoveObject(Vector3 direction);
    protected virtual void OnOutsideScreen() { }
    protected virtual void OnInsideScreen() { }

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

    public void Release(Vector3 direction, float speedMultiplier = 1)
    {
        _speedMultiplier = speedMultiplier;
        MoveObject(direction);
    }

    protected virtual void DeactivateMovingObject()
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
