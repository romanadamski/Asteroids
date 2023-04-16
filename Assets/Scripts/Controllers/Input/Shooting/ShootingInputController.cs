using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingInputController : BaseInputController
{
    [SerializeField]
    private KeyCode shootKey = KeyCode.Space;
    [SerializeField]
    private string bulletTypeName;
    [SerializeField]
    private int selectedTypeIndex;

    public override void OnUpdate()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            ReleaseBullet();
            EventsManager.Instance.OnBulletFired();
        }
        if (Input.GetKeyDown(shootKey))
        {
            ReleaseBullet();
            EventsManager.Instance.OnBulletFired();
        }
    }

    private void ReleaseBullet()
    {
        var bullet = GetBulletFromPool();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true);

        if (!CheckBulletBeginningPosition(bullet))
        {
            Debug.LogError($"Start shooting position {name.Bold()} is too close to shooter!");
            ObjectPoolingManager.Instance.ReturnToPool(bullet.gameObject.GetComponent<BasePoolableController>());
            return;
        }

        bullet.Release(transform.up);
    }

    private BaseBulletMovementController GetBulletFromPool()
    {
        return ObjectPoolingManager.Instance.GetFromPool(bulletTypeName).GetComponent(bulletTypeName) as BaseBulletMovementController;
    }

    private bool CheckBulletBeginningPosition(BaseBulletMovementController bullet)
    {
        //bullets without collider cannot cause collision
        if (!bullet.TryGetComponent(out Collider2D bulletCollider))
        {
            return true;
        }

        //if firing object is moving, do not check collision
        var parentRigidbody = GetComponentInParent<Rigidbody2D>();
        if (parentRigidbody != null && parentRigidbody.velocity.magnitude > 0)
        {
            return true;
        }

        List<Collider2D> results = new List<Collider2D>();
        if (bulletCollider.OverlapCollider(new ContactFilter2D().NoFilter(), results) > 0
            && results.Any(x => x.gameObject.tag.Equals(GameObjectTagsConstants.PLAYER)))
        {
            return false;
        }

        return true;
    }
}
