using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public abstract class Units : MonoBehaviour
{
    #region Unit Stats
    // unit stats
    [SerializeField] string _name = "Unit";
    [SerializeField] float _damage = 2.0f;
    [SerializeField] float _maxHealth = 10f;
    [SerializeField] float _armor = 1.0f;
    [SerializeField] float _attackCD = 2f;
    [SerializeField] public Sprite _skill1Image;
    [SerializeField] public Sprite _skill2Image;
    [SerializeField] public int _honor = 1;

    [SerializeField] public string _job1name;
    [SerializeField] public string _job2name;
    [SerializeField] public string _job3name;
    [SerializeField] public string _job4name;
    [SerializeField] public string _job5name;
    [SerializeField] public string _job6name;
    [SerializeField] public string _job7name;
    [SerializeField] public string _job8name;
    [SerializeField] public string _job9name;
    [SerializeField] public string _job10name;
    #endregion

    #region Action Interfaces
    // actions interfaces
    public IMovement _movement;
    public ITargeted _targeted;
    public ITakeDamage _takedamage;
    public IDoDamage _dodamage;
    public IIngameUI _ingameUI;
    public IActions _actions;
    public IUnitJob _unitJob;
    #endregion

    #region Misc Variables
    // unit mesh
    [SerializeField] Outline _meshOutline;
    // radius attack point
    [SerializeField] float _weaponRadius = 1.5f;
    // enemy layer
    [SerializeField] public LayerMask _enemyLayer;

    // misc
    private NavMeshAgent _navMeshAgent;
    private TMP_Text _namefield;
    private int _unitLayer;
    [HideInInspector] public bool _isAiEnabled = false;
    private bool _selectedMesh = false;
    private int _damagableFound;
    private readonly Collider[] _weapon_colliders = new Collider[1];
    private bool canAttack = true;
    private int _playerLayer = 7;
    [HideInInspector] public float _currentHealth;
    private bool _canUpdateAi = true;
    private float _canUpdateCd = 0.3f;
    private bool _canRegen = true;
    #endregion

    private void OnEnable()
    {
        InitUnitActions();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _namefield = GameObject.FindGameObjectWithTag("NameField").GetComponent<TMP_Text>();
        _unitLayer = gameObject.layer;
        _currentHealth = _maxHealth;
    }

    public virtual void InitUnitActions()
    {
        _movement = new DefaultMovement();
        _targeted = new DefaultTargeted();
        _takedamage = new DefaultTakeDamage();
        _dodamage = new DefaultDoDamage();
        _ingameUI = GameObject.FindGameObjectWithTag("UIPanel").GetComponent<DefaultIngameUI>();
        _actions = new NoActions();
        _unitJob = new NoJob();
    }

    private void FixedUpdate()
    {
        if (_isAiEnabled && _canUpdateAi)
        {
            StartCoroutine(CanUpdateAi());
            _movement.StateChoise();
        }

        // в переменные записывается количество пересечений созданного коллайдера точки и объектов, с которыми возможно взаимодействие
        _damagableFound = Physics.OverlapSphereNonAlloc(gameObject.transform.position, _weaponRadius, _weapon_colliders, _enemyLayer);

        // если враг обнаружил и атакует игрока и нет кулдауна атаки, то вызывать метод атаки
        if (_movement.IsAttacking() && canAttack)
        {
            StartCoroutine(DoDamage());
        }
    }

    #region Movement Part
    // ==============================
    //        Movement Part
    // ==============================
    public void MoveUnit(Vector3 position)
    {
        if (_navMeshAgent)
        {
            _isAiEnabled = false;
            _movement.SetDestination(_navMeshAgent, position);
        }
    }

    public void StartAI(Vector3 position)
    {
        if (_navMeshAgent)
        {
            _movement.StartAI(_navMeshAgent, _enemyLayer, gameObject, position);
            _isAiEnabled = true;
        }
    }

    private IEnumerator CanUpdateAi()
    {
        _canUpdateAi = false;
        yield return new WaitForSeconds(_canUpdateCd);
        _canUpdateAi = true;
    }
    #endregion

    #region Damage Part
    // ==============================
    //        Damage Part
    // ==============================
    public void TakeDamage(float damage)
    {
        _currentHealth = _takedamage.OnTakeDamage(_currentHealth, _armor, damage);
        if (_selectedMesh) _ingameUI.SetStatsPanel(_name, _currentHealth, _maxHealth, _damage, _armor);
    }

    private IEnumerator DoDamage()
    {
        if (_damagableFound > 0)
        {
            canAttack = false;
            Units _interactable = _weapon_colliders[0].GetComponent<Units>();
            _dodamage.OnDoDamage(_interactable, _damage);
            yield return new WaitForSeconds(_attackCD);
            canAttack = true;
        }
    }
    #endregion

    #region Selection Outline Part
    // ==============================
    //     Selection Outline Part
    // ==============================
    public void SelectUnit(bool isFriendly)
    {
        if (isFriendly)
        {
            _targeted.SetOutline(_meshOutline, true, Color.yellow, 5f);
            _ingameUI.SetStatsPanel(_name, _currentHealth, _maxHealth, _damage, _armor);
            _ingameUI.SetActionsPanel(this);
            if (gameObject.tag == "Worker") _ingameUI.SetBuildingActions(this);
            gameObject.layer = _playerLayer;
        }
        else
        {
            _targeted.SetOutline(_meshOutline, true, Color.magenta, 5f);
        }
        _selectedMesh = true;
        _namefield.text = "";
    }

    public void DeselectUnit()
    {
        _targeted.SetOutline(_meshOutline, false, Color.white, 2f);
        _ingameUI.UnsetActionPanel(this);
        _ingameUI.UnsetBuildingActions(this);
        _selectedMesh = false;
        gameObject.layer = _unitLayer;
    }
    #endregion

    #region Mouseover Outline Part
    // ==============================
    //     Mouseover Outline Part
    // ==============================
    public void OnMouseOver()
    {
        if (!_selectedMesh)
        {
            _targeted.SetOutline(_meshOutline, true, Color.white, 2f);
            _namefield.transform.position = new Vector3(Input.mousePosition.x + 100, Input.mousePosition.y, Input.mousePosition.z);
            _namefield.text = _name;
        }
    }

    private void OnMouseExit()
    {
        if (!_selectedMesh)
        {
            _targeted.SetOutline(_meshOutline, false, Color.white, 2f);
            _namefield.text = "";
        }
    }
    #endregion

    #region Misc Part
    // ==============================
    //          Misc Part
    // ==============================

    // отрисовывает радиус коллайдеров точек взаимодействия, просто для визуального удобства.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, _weaponRadius);
    }
    #endregion

    #region Unit Actions Part
    // =============================
    //      Unit Actions Part
    // =============================

    // кнопки действий для юнита
    public void FirstSkill()
    {
        _actions.FirstSkill(this);
    }

    public void SecondSkill()
    {
        _actions.SecondSkill(this);
    }

    public void RepairUnit()
    {
        if ((_currentHealth < _maxHealth) && _canRegen)
        {
            _canRegen = false;
            StartCoroutine(RepairCd());
        }     
    }

    private IEnumerator RepairCd()
    {
        yield return new WaitForSeconds(10);
        _actions.UnitRepair(this);
        _ingameUI.SetStatsPanel(_name, _currentHealth, _maxHealth, _damage, _armor);
        _canRegen = true;
        RepairUnit();
    }

    public void DeleteUnit()
    {
        _actions.UnitDestroy(this);
        _ingameUI.SetResources(BaseResources.onBase);
    }
    #endregion

    #region Unit Job Part
    // =============================
    //      Unit Job Part
    // =============================

    // кнопки работы для юнита
    public void Job1() 
    {
        _unitJob.Job1(this);
    }
    public void Job2()
    {
        _unitJob.Job2(this);
    }
    public void Job3()
    {
        _unitJob.Job3(this);
    }
    public void Job4()
    {
        _unitJob.Job4(this);
    }
    public void Job5()
    {
        _unitJob.Job5(this);
    }
    public void Job6()
    {
        _unitJob.Job6(this);
    }
    public void Job7()
    {
        _unitJob.Job7(this);
    }
    public void Job8()
    {
        _unitJob.Job8(this);
    }
    public void Job9()
    {
        _unitJob.Job9(this);
    }
    public void Job10()
    {
        _unitJob.Job10(this);
    }
    #endregion
}
