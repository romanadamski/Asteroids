using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(StartShootingPositionController))]
public class ShootingInputController : BaseInputController
{
    private StartShootingPositionController _startShootingPosition;

    [SerializeField]
    private KeyCode shootKey = KeyCode.Space;
    [SerializeField]
    private string bulletTypeName;
    [SerializeField]
    private int selectedTypeIndex;

    private void Awake()
    {
        _startShootingPosition = GetComponent<StartShootingPositionController>();
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(shootKey))
        {
            ReleaseBullet();
        }
    }

    private void ReleaseBullet()
    {
        var bullet = GetBulletFromPool();
        bullet.transform.position = _startShootingPosition.transform.position;
        bullet.transform.rotation = _startShootingPosition.transform.rotation;
        bullet.gameObject.SetActive(true);

        if (!CheckBulletBeginningPosition(bullet))
        {
            Debug.LogError($"Start shooting position {name.Bold()} is too close to shooter!");
            ObjectPoolingManager.Instance.ReturnToPool(bullet.gameObject.GetComponent<BasePoolableController>());
            return;
        }

        bullet.Release();
    }

    private BaseBulletMovementController GetBulletFromPool()
    {
        return ObjectPoolingManager.Instance.GetFromPool(bulletTypeName).GetComponent(bulletTypeName) as BaseBulletMovementController;
    }

    private bool CheckBulletBeginningPosition(BaseBulletMovementController bullet)
    {
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        if (!bulletCollider)
        {
            return true;
        }

        List<Collider2D> results = new List<Collider2D>();
        if (bulletCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results) > 0
            && results.Any(x => x.gameObject.tag.Equals(GameObjectTags.PLAYER)))
        {
            return false;
        }

        return true;
    }

    public override void OnFixedUpdate()
    {
        
    }
}
