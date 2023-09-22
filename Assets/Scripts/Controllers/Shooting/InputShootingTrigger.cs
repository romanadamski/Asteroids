using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputShootingTrigger : ShootingTrigger
{
    [SerializeField]
    private KeyCode shootKey = KeyCode.Space;

    protected override void SetTrigger()
    {
        if (Input.GetKeyDown(shootKey))
        {
            TriggerShoot = true;
        }
        if (Input.GetKeyUp(shootKey))
        {
            TriggerShoot = false;
        }
    }
}
