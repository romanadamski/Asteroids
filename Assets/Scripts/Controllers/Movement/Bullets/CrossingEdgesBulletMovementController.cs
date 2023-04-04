﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CrossingEdgesBulletMovementController : BaseBulletMovementController
{
    public override void OnOutsideScreen()
    {
        ScreenManager.Instance.HandleScreenEdgeCrossing(transform);
    }
}