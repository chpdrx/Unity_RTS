using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DefaultKeyboardActions : IKeyboard
{
    public List<Units> OnEscapePressed(Units _unit, Units _currentUnit, Units _heroUnit)
    {
        _unit.DeselectUnit();
        _currentUnit.DeselectUnit();
        _unit = _heroUnit;
        _currentUnit = null;
        _unit.SelectUnit(true);
        return new List<Units>() { _unit, _currentUnit };
    }

    public void OnSpacePressed(Units _unit, CinemachineVirtualCamera _virtualCamera)
    {
        _virtualCamera.Follow = _unit.transform;
        _virtualCamera.LookAt = _unit.transform;
    }
}
