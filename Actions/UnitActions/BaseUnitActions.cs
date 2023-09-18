using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitActions : IActions
{
    public virtual void FirstSkill(Units unit) {}

    public virtual void SecondSkill(Units unit) {}

    public void UnitDestroy(Units unit)
    {
        BaseResources.honor += unit._honor;
        unit.TakeDamage(9999);
    }

    public void UnitRepair(Units unit)
    {
        if (BaseResources.onBase)
        {
            unit._currentHealth += BaseResources.unitRegenValue;
        }
        else
        {
            unit._currentHealth += MapResources.unitRegenValue;
        }
    }
}
