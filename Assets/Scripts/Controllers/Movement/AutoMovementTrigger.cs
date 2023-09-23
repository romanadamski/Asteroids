using UnityEngine;

public class AutoMovementTrigger : MovementTrigger
{
    [Range(1, 5)]
    [SerializeField]
    private float changeMovementFrequency;

    private float _timeElapsed;

    private void OnEnable()
    {
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
        XAxis = Random.Range(-1f, 1f);
        YAxis = Random.Range(-1f, 1f);
    }
}
