using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActions
{
    public void FirstSkill(Units unit);
    public void SecondSkill(Units unit);
    public void UnitRepair(Units unit);
    public void UnitDestroy(Units unit);
}
