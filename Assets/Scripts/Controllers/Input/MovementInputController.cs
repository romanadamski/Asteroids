using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class MovementInputController : BaseInputController
{
    private readonly string HORIZONTAL_AXIS_NAME = "Horizontal";
    private readonly string VERTICAL_AXIS_NAME = "Vertical";

    [SerializeField]
    [Range(1, 10)]
    private float speedMultiplier = 5;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private float _xAxis;
    private float _yAxis;

    private Vector2 MovementAxis => new Vector2(CalculateAxis(_xAxis), CalculateAxis(_yAxis));

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            StopMovement();
        }

        _xAxis = Input.GetAxis(HORIZONTAL_AXIS_NAME);
        _yAxis = Input.GetAxis(VERTICAL_AXIS_NAME);
    }

    public override void OnFixedUpdate()
    {
        MoveByArrows();
        Rotate();
    }

    private float CalculateAxis(float axis)
    {
        return axis * GameSettingsManager.Instance.Settings.PlayerMovementSpeed * (speedMultiplier / 5);
    }

    private void StopMovement()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity= 0;
    }

    private void MoveByArrows()
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
}
