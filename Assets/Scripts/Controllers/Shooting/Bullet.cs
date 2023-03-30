using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float _bulletSpeedMultiplier = 1;

    protected bool _isReleased;
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;

    public abstract void OnScreenEdgesCrossed();

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
            OnScreenEdgesCrossed();
        }
    }

    private void FixedUpdate()
    {
        if (!_isReleased) return;
        _rigidbody2D.velocity = transform.up * Managers.SettingsManager.Settings.BaseBulletMovementSpeed * _bulletSpeedMultiplier;
    }

    public void Release()
    {
        _isReleased = true;
    }
}
