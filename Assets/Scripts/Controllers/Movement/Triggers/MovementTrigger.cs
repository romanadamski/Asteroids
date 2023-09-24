using System;
using UnityEngine;

public enum TriggerType
{
    Continuous,
    Single
}

public abstract class MovementTrigger : MonoBehaviour
{
    [SerializeField]
    private TriggerType triggerType;

    public event Action HandleMovement;

    public float XDirection { get; protected set; }
    public float YDirection { get; protected set; }
    public Vector2 MovementDirection => new Vector2(XDirection, YDirection);

    protected abstract void SetAxis();

    private void Update()
    {
        if (triggerType == TriggerType.Single) return;

        SetAxis();
    }

    private void FixedUpdate()
    {
        if (triggerType == TriggerType.Single) return;

        HandleMovement?.Invoke();
    }

    protected virtual void OnEnable()
    {
        if (triggerType == TriggerType.Single)
        {
            SetAxis();
            HandleMovement?.Invoke();
        }
    }
}
