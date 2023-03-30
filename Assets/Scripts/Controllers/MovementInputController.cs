using System;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class MovementInputController : InputController
{
    private readonly string HORIZONTAL_AXIS_NAME = "Horizontal";
    private readonly string VERTICAL_AXIS_NAME = "Vertical";

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
            //todo where to put player death
            Managers.EventsManager.OnPlayerDeath(name);
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
        return axis * Managers.SettingsManager.Settings.PlayerMovementSpeed;
    }

    private void StopMovement()
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity= 0;
    }

    private void MoveByArrows()
    {
        _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, MovementAxis, Time.deltaTime * Managers.SettingsManager.Settings.PlayerMovementPrecision);
        ManageScreenEdges();
    }

    private void ManageScreenEdges()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (!GeometryUtility.TestPlanesAABB(planes, _spriteRenderer.bounds))
        {
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPos.y < 0)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                    screenPos.x,
                    Screen.height,
                    screenPos.z));
            }
            else if (screenPos.y > Screen.height)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                    screenPos.x,
                    0,
                    screenPos.z));
            }

            if (screenPos.x < 0)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    screenPos.y,
                    screenPos.z));
            }
            else if (screenPos.x > Screen.width)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                    0,
                    screenPos.y,
                    screenPos.z));
            }
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
                Time.deltaTime * Managers.SettingsManager.Settings.PlayerRotationSpeed);
        }
    }
}
