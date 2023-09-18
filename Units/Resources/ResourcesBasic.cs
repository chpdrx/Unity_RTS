using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesBasic : Units
{
    public override void InitUnitActions()
    {
        base.InitUnitActions();
        _movement = new NoMovement();
        _dodamage = new NoDoDamage();
        _takedamage = new ResourceTakeDamage();
    }

    public new void OnMouseOver() { }

    private void OnMouseExit() { }
}
