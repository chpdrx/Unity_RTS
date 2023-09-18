using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuildingAction : IActions
{
    public void FirstSkill(Units unit) {}

    public void SecondSkill(Units unit) {}

    public void UnitDestroy(Units unit)
    {
        throw new System.NotImplementedException();
    }

    public void UnitRepair(Units unit)
    {
        throw new System.NotImplementedException();
    }
}
