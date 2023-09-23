using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public abstract class BaseMovementController : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField]
    protected float speedMultiplier;

    private SpriteRenderer _spriteRenderer;
    private Plane[] _cameraPlanes;

    protected Rigidbody2D _rigidbody2D;

    protected MovementTrigger MovementTrigger { get; private set; }

    protected abstract void HandleMovement();
    protected virtual void OnOutsideScreen() { }
    protected virtual void OnInsideScreen() { }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        MovementTrigger = GetComponent<MovementTrigger>();
        _cameraPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (!MovementTrigger)
        {
            Debug.LogError($"There is no {typeof(MovementTrigger).Name} attached to object {name}!");
            return;
        }

        MovementTrigger.HandleMovement += HandleMovement;
    }

    private void Update()
    {
        if (!GeometryUtility.TestPlanesAABB(_cameraPlanes, _spriteRenderer.bounds))
        {
            OnOutsideScreen();
        }
        else
        {
            OnInsideScreen();
        }
    }

    protected void DeactivateMovingObject()
    {
        StopMovement();
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
    }

    protected void StopMovement()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
    }
}
