using UnityEngine;

public class RandomMovementTrigger : MovementTrigger
{
    [Tooltip("In seconds")]
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float changeMovementFrequency;

    private float _timeElapsed;

    protected override void OnEnable()
    {
        base.OnEnable();

        SetRandomAxis();
    }

    protected override void SetAxis()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed < changeMovementFrequency) return;
        
        SetRandomAxis();
        _timeElapsed = 0;
    }

    private void SetRandomAxis()
    {
        var randomDirection = RandomizeHelper.GetRandomDirectionDependsOnPosition(transform);
        XAxis = randomDirection.x;
        YAxis = randomDirection.y;
    }
}
