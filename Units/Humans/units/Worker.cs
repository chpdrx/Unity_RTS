using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Units
{
    public override void InitUnitActions()
    {
        base.InitUnitActions();
        _actions = new WorkerActions();
        _unitJob = new WorkerJob();
    }
}
