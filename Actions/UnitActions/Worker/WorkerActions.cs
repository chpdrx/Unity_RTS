using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerActions : BaseUnitActions
{
    public override void FirstSkill(Units unit)
    {
        unit._enemyLayer = LayerMask.GetMask("Enemy");
    }

    public override void SecondSkill(Units unit)
    {
        if (unit._enemyLayer.value == LayerMask.GetMask("Wood"))
        {
            unit._enemyLayer = LayerMask.GetMask("Iron");
            unit._ingameUI.UpdatePanels(unit);
        }
        else if (unit._enemyLayer.value == LayerMask.GetMask("Iron"))
        { 
            unit._enemyLayer = LayerMask.GetMask("Wood");
            unit._ingameUI.UpdatePanels(unit);
        }
    }
}
