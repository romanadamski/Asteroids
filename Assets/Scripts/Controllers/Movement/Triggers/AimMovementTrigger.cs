using UnityEngine;

public class AimMovementTrigger : MovementTrigger
{
    [SerializeField]
    private string targetTag;

    private GameObject[] _targets;

    private void Awake()
    {
        EventsManager.Instance.GameplayStarted += GameplayStarted;
    }

    private void GameplayStarted()
    {
        GetTargetsByTag();
    }

    protected override void SetAxis()
    {
        var closest = GetClosesTarget(transform.position);
        if (!closest) return;

        var direction = closest.transform.position - transform.position;

        XDirection = direction.normalized.x;
        YDirection = direction.normalized.y;
    }

    private void GetTargetsByTag()
    {
        _targets = GameObject.FindGameObjectsWithTag(targetTag);
    }

    public GameObject GetClosesTarget(Vector3 position)
    {
        GameObject closest = null;
        var minDist = Mathf.Infinity;

        foreach (var enemy in _targets)
        {
            if (!enemy.activeSelf) continue;
            if (enemy.Equals(gameObject)) continue;

            var distance = Vector3.Distance(position, enemy.transform.position);
            if (distance < minDist)
            {
                minDist = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}
