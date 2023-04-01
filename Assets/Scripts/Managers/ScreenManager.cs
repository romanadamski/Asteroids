using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    ScreenEdgesHelper _screenEdgesHelper;

    private void Awake()
    {
        _screenEdgesHelper = new ScreenEdgesHelper();
    }

    public void HandleScreenEdgeCrossing(Transform transform)
    {
        _screenEdgesHelper.HandleScreenEdgeCrossing(transform);
    }
}
