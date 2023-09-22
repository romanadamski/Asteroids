using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovementTrigger : MovementTrigger
{
    [Range(1,10)]
    [SerializeField]
    private int changeMovementDirectionFrequency;

    protected override void SetAxis()
    {
        if (Time.frameCount % (20 * changeMovementDirectionFrequency) != 0) return;

        XAxis = Random.Range(-1f, 1f);
        YAxis = Random.Range(-1f, 1f);
    }
}
