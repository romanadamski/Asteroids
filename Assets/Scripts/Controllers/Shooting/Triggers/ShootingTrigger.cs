using System;
using UnityEngine;

public abstract class ShootingTrigger : MonoBehaviour
{
    public event Action OnUpdate;

    public bool TriggerShoot { get; protected set; }

    protected abstract void SetTrigger();

    private void Update()
    {
        SetTrigger();
        OnUpdate?.Invoke();
    }
}
