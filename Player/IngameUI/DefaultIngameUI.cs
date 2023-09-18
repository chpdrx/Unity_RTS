using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DefaultIngameUI : MonoBehaviour, IIngameUI
{
    [SerializeField] UIDocument resourcesPanel;
    [SerializeField] UIDocument statsPanel;
    [SerializeField] UIDocument minimapPanel;
    [SerializeField] UIDocument actionPanel;
    [SerializeField] UIDocument buildingsPanel;

    private VisualElement resourcesRoot;
    private VisualElement statsRoot;
    private VisualElement minimapRoot;
    private VisualElement actionsRoot;
    private VisualElement buildingsRoot;


    private Label _nameUI;
    private Label _healthUI;
    private Label _damageUI;
    private Label _armorUI;
    private Label _levelUI;

    private VisualElement _minimapUI;

    private Button _skill1;
    private Button _skill2;
    private Button _repair;
    private Button _deleteUnit;

    private Label _woodUI;
    private Label _ironUI;
    private Label _honorUI;
    private Label _villagersUI;
    private Label _upPointsUI;

    private Button _job1;
    private Button _job2;
    private Button _job3;
    private Button _job4;
    private Button _job5;
    private Button _job6;
    private Button _job7;
    private Button _job8;
    private Button _job9;
    private Button _job10;
    private VisualElement _jobPanel;


    private void Start()
    {
        resourcesRoot = resourcesPanel.rootVisualElement;
        statsRoot = statsPanel.rootVisualElement;
        minimapRoot = minimapPanel.rootVisualElement;
        actionsRoot = actionPanel.rootVisualElement;
        buildingsRoot = buildingsPanel.rootVisualElement;

        _woodUI = resourcesRoot.Q<Label>("WoodValue");
        _ironUI = resourcesRoot.Q<Label>("IronValue");
        _honorUI = resourcesRoot.Q<Label>("HonorValue");
        _villagersUI = resourcesRoot.Q<Label>("PeopleValue");
        _upPointsUI = resourcesRoot.Q<Label>("UpPointsValue");
        SetResources(BaseResources.onBase);

        _nameUI = statsRoot.Q<Label>("Name");
        _healthUI = statsRoot.Q<Label>("Health");
        _damageUI = statsRoot.Q<Label>("Damage");
        _armorUI = statsRoot.Q<Label>("Armor");
        _levelUI = statsRoot.Q<Label>("Level");

        _minimapUI = minimapRoot.Q<VisualElement>("MiniMap");

        _skill1 = actionsRoot.Q<Button>("Action1");
        _skill2 = actionsRoot.Q<Button>("Action2");
        _repair = actionsRoot.Q<Button>("Action3");
        _deleteUnit = actionsRoot.Q<Button>("Action4");

        _job1 = buildingsRoot.Q<Button>("Job1");
        _job2 = buildingsRoot.Q<Button>("Job2");
        _job3 = buildingsRoot.Q<Button>("Job3");
        _job4 = buildingsRoot.Q<Button>("Job4");
        _job5 = buildingsRoot.Q<Button>("Job5");
        _job6 = buildingsRoot.Q<Button>("Job6");
        _job7 = buildingsRoot.Q<Button>("Job7");
        _job8 = buildingsRoot.Q<Button>("Job8");
        _job9 = buildingsRoot.Q<Button>("Job9");
        _job10 = buildingsRoot.Q<Button>("Job10");
        _jobPanel = buildingsRoot.Q<VisualElement>("Panel");
    }

    public void SetStatsPanel(string name, float currentHealth, float maxHealth, float damage, float armor)
    {
        _nameUI.text = name;
        _healthUI.text = currentHealth + "/" + maxHealth;
        _damageUI.text = damage.ToString();
        _armorUI.text = armor.ToString();
        _levelUI.text = ((maxHealth + damage + armor) / 13).ToString();
    }

    public void SetActionsPanel(Units unit)
    {
        _skill1.style.backgroundImage = new StyleBackground(unit._skill1Image);
        _skill2.style.backgroundImage = new StyleBackground(unit._skill2Image);
        _skill1.clicked += unit.FirstSkill;
        _skill2.clicked += unit.SecondSkill;
        _repair.clicked += unit.RepairUnit;
        _deleteUnit.clicked += unit.DeleteUnit;
    }

    public void UpdatePanels(Units unit)
    {
        _skill2.text = LayerMask.LayerToName((int)Math.Log(unit._enemyLayer.value, 2));
    }

    public void UnsetActionPanel(Units unit)
    {
        _skill1.clicked -= unit.FirstSkill;
        _skill2.clicked -= unit.SecondSkill;
        _repair.clicked -= unit.RepairUnit;
        _deleteUnit.clicked -= unit.DeleteUnit;
    }

    public void SetBuildingActions(Units unit)
    {
        _job1.clicked += unit.Job1;
        _job2.clicked += unit.Job2;
        _job3.clicked += unit.Job3;
        _job4.clicked += unit.Job4;
        _job5.clicked += unit.Job5;
        _job6.clicked += unit.Job6;
        _job7.clicked += unit.Job7;
        _job8.clicked += unit.Job8;
        _job9.clicked += unit.Job9;
        _job10.clicked += unit.Job10;

        _job1.text = unit._job1name;
        _job2.text = unit._job2name;
        _job3.text = unit._job3name;
        _job4.text = unit._job4name;
        _job5.text = unit._job5name;
        _job6.text = unit._job6name;
        _job7.text = unit._job7name;
        _job8.text = unit._job8name;
        _job9.text = unit._job9name;
        _job10.text = unit._job10name;

        _jobPanel.style.visibility = Visibility.Visible;
    }

    public void UnsetBuildingActions(Units unit)
    {
        _job1.clicked -= unit.Job1;
        _job2.clicked -= unit.Job2;
        _job3.clicked -= unit.Job3;
        _job4.clicked -= unit.Job4;
        _job5.clicked -= unit.Job5;
        _job6.clicked -= unit.Job6;
        _job7.clicked -= unit.Job7;
        _job8.clicked -= unit.Job8;
        _job9.clicked -= unit.Job9;
        _job10.clicked -= unit.Job10;

        _jobPanel.style.visibility = Visibility.Hidden;
    }

    public void SetMiniMap()
    {
        throw new System.NotImplementedException();
    }

    public void SetResources(bool onBase)
    {
        if (onBase)
        {
            _woodUI.text = BaseResources.wood.ToString();
            _ironUI.text = BaseResources.iron.ToString();
            _villagersUI.text = BaseResources.currentVillagers.ToString() + '/' + BaseResources.maxVillagers.ToString(); 
        }
        else
        {
            _woodUI.text = MapResources.wood.ToString();
            _ironUI.text = MapResources.iron.ToString();
            _villagersUI.text = MapResources.currentVillagers.ToString() + '/' + BaseResources.maxVillagers.ToString();
        }
        _honorUI.text = BaseResources.honor.ToString();
        _upPointsUI.text = BaseResources.upPoint.ToString();
    }
}
