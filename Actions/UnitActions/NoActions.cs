using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoActions : IActions
{
    public void FirstSkill(Units unit) { }

    public void SecondSkill(Units unit) { }

    public void UnitDestroy(Units unit) { }

    public void UnitRepair(Units unit) { }
}
