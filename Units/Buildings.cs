using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildings : Units
{
    public override void InitUnitActions()
    {
        base.InitUnitActions();
        _movement = new NoMovement();
        _dodamage = new NoDoDamage();
    }
}
