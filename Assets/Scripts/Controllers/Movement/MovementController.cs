using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(MovementTrigger))]
public class MovementController : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private float speedMultiplier = 5;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private MovementTrigger _movementTrigger;

    private Vector2 MovementAxis => new Vector2(CalculateAxis(_movementTrigger.XAxis), CalculateAxis(_movementTrigger.YAxis));

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movementTrigger = GetComponent<MovementTrigger>();

        _movementTrigger.OnFixedUpdate += OnFixedUpdate;
    }

    public void OnFixedUpdate()
    {
        Move();
        Rotate();
        Debug.Log($"{name} MOVE {_rigidbody2D.velocity} ROTATE {transform.rotation}");
    }

    private float CalculateAxis(float axis)
    {
        return axis * GameSettingsManager.Instance.Settings.PlayerMovementSpeed * (speedMultiplier / 5);
    }

    //todo why after disable and enable only one axis is taken if i do not release keys?
    private void StopMovement()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
    }

    private void Move()
    {
        _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, MovementAxis, Time.deltaTime * GameSettingsManager.Instance.Settings.PlayerMovementPrecision);
        ManageScreenEdges();
    }

    private void ManageScreenEdges()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (!GeometryUtility.TestPlanesAABB(planes, _spriteRenderer.bounds))
        {
            ScreenManager.Instance.HandleScreenEdgeCrossing(transform);
        }
    }

    private void Rotate()
    {
        if (_rigidbody2D.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(-_rigidbody2D.velocity.x, _rigidbody2D.velocity.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward),
                Time.deltaTime * GameSettingsManager.Instance.Settings.PlayerRotationSpeed);
        }
    }

    protected void OnDisable()
    {
        StopMovement();
    }
}
