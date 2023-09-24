using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMovementTrigger : MovementTrigger
{
    protected override void SetAxis()
    {
        var closest = GameplayManager.Instance.GetClosestEnemy(transform.position);
        if (!closest) return;

        var direction = closest.transform.position - transform.position;

        XDirection = direction.normalized.x;
        YDirection = direction.normalized.y;
    }
}
