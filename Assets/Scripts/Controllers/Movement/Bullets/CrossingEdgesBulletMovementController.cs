using UnityEngine;

public class CrossingEdgesBulletMovementController : BaseBulletMovementController
{
    [Range(1, 5)]
    [SerializeField]
    private uint maxCrossingEdgeCount;

    private uint _currentCrossingEdgeCount;

    protected override void OnOutsideScreen()
    {
        if (_currentCrossingEdgeCount.Equals(maxCrossingEdgeCount))
        {
            DeactivateMovingObject();
            return;
        }
        
        ScreenManager.Instance.HandleScreenEdgeCrossing(transform);
        _currentCrossingEdgeCount++;
    }
}