using System;
using UnityEngine;

public abstract class ShootingTrigger : MonoBehaviour
{
    public event Action OnFixedUpdate;

    public bool TriggerShoot { get; protected set; }

    protected abstract void SetTrigger();

    private void Update()
    {
        SetTrigger();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
