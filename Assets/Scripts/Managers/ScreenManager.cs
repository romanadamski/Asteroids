using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenManager : BaseManager<ScreenManager>
{
    ScreenEdgesHelper _screenEdgesHelper;

    private void Start()
    {
        _screenEdgesHelper = new ScreenEdgesHelper();
    }

    public void HandleScreenEdgeCrossing(Transform transform)
    {
        _screenEdgesHelper.HandleScreenEdgeCrossing(transform);
    }
}
