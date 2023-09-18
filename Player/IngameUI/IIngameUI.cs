using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IIngameUI
{
    public void SetStatsPanel(string _name, float _currentHealth, float _maxHealth, float _damage, float _armor);
    public void SetActionsPanel(Units unit);
    public void UnsetActionPanel(Units unit);
    public void SetMiniMap();
    public void SetResources(bool onBase);
    public void SetBuildingActions(Units unit);
    public void UnsetBuildingActions(Units unit);
    public void UpdatePanels(Units unit);
}
