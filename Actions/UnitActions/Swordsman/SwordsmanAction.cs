using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanAction : BaseUnitActions
{
    private float _skillCd = 180;
    private bool _canUseFirstSkill = true;

    public override void FirstSkill(Units unit)
    {
        
    }

    private IEnumerator FirstSkillCds(Units unit)
    {
        _canUseFirstSkill = false;
        unit._honor *= 2;
        yield return new WaitForSeconds(30);
        unit._honor /= 2;
        yield return new WaitForSeconds(_skillCd - 30);
        _canUseFirstSkill = true;
    }

    public override void SecondSkill(Units unit)
    {
        
    }
}
