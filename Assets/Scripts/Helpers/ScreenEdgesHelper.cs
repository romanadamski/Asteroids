using UnityEngine;

public class ScreenEdgesHelper
{
    /// <summary>
    /// Show object at opposite side of scrren after crossing screen edge
    /// </summary>
    /// <param name="objectTransform">Object which crossing screen's edge</param>
    public void HandleScreenEdgeCrossing(Transform objectTransform)
    {
        var screenPos = Camera.main.WorldToScreenPoint(objectTransform.position);
        if (screenPos.y < 0)
        {
            objectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                screenPos.x,
                Screen.height,
                screenPos.z));
        }
        else if (screenPos.y > Screen.height)
        {
            objectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                screenPos.x,
                0,
                screenPos.z));
        }

        if (screenPos.x < 0)
        {
            objectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                Screen.width,
                screenPos.y,
                screenPos.z));
        }
        else if (screenPos.x > Screen.width)
        {
            objectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(
                0,
                screenPos.y,
                screenPos.z));
        }
    }
}
