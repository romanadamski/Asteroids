using System;
using UnityEngine;

public abstract class MovementTrigger : MonoBehaviour
{
    public event Action OnFixedUpdate;

    public float XAxis { get; protected set; }
    public float YAxis { get; protected set; }

    protected abstract void SetAxis();

    private void Update()
    {
        SetAxis();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
