using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingInputController : InputController
{
    [SerializeField]
    private Transform shootStartCoordinates;
    [SerializeField]
    private string bulletTag;
    [SerializeField]
    KeyCode shootKey = KeyCode.Space;

    public override void OnFixedUpdate()
    {
        
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
        bullet.transform.position = shootStartCoordinates.position;
        bullet.transform.rotation = shootStartCoordinates.rotation;
        bullet.gameObject.SetActive(true);
        bullet.Release();
    }

    private Bullet GetBulletFromPool()
    {
        return Managers.ObjectPoolingManager.GetFromPool(bulletTag).GetComponent<Bullet>();
    }
}
