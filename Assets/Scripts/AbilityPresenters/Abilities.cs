using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using BlobArena.Model;
using UnityEditor;
using System;

public class Abilities : MonoBehaviour
{
    public const int MaxCard = 3;
    public const int MaxPassiveAbilities = 4;
    public const int MaxActiveAbilities = 4;

    [SerializeField] private List<AbilityPresenter> _firstAbilityPresenters;
    [SerializeField] private List<AbilityPresenter> _abilityPresenters;
    [SerializeField] private AbilitySpawner _abilitySpawner;
    [SerializeField] private AbilitiesView _view;
    [SerializeField] private AbilityCardPresneter _template;

    private List<AbilityPresenter> _selectedAbilities = new List<AbilityPresenter>();
    private List<IUpdatable> _updatableAbilities;
    private int _aciveCount = 0;
    private int _passiveCount = 0;

    public event UnityAction<AbilityCardPresneter> Spawned;
    public event UnityAction<AbilityPresenter> Upgraded;
    public event UnityAction<AbilityPresenter> Collected;

    public IEnumerable<AbilityInfo> SelectedAbilitied => _selectedAbilities.Select(ability => ability.AbilityInfo);
    public bool Contains(AbilityPresenter ability) => _selectedAbilities.Contains(ability);
    public int ActiveCount => _selectedAbilities.Count(ability => ability.AbilityInfo.Type == AbilityType.Active);
    public int PassiveCount => _selectedAbilities.Count(ability => ability.AbilityInfo.Type == AbilityType.Passive);

    private void Awake()
    {
        _updatableAbilities = _abilityPresenters.FindAll(ability => ability is IUpdatable).Cast<IUpdatable>().ToList();
    }

    private void OnEnable()
    {
        _abilitySpawner.CanSpawn += SpawnAbility;
    }

    private void OnDisable()
    {
        _abilitySpawner.CanSpawn -= SpawnAbility;
    }

    private void Start()
    {
        SpawnRandomAbility(_firstAbilityPresenters, transform.position + Vector3.forward * 5f);
    }

    private void Update()
    {
        foreach (var ability in _updatableAbilities)
            ability.Tick(Time.deltaTime);
    }

    public void AddAbility(AbilityPresenter ability)
    {
        if (ability.AbilityInfo.Level == 0)
        {
            _selectedAbilities.Add(ability);
            _view.Add(ability);

            if (ability.AbilityInfo.Type == AbilityType.Active)
                _aciveCount++;
            else
                _passiveCount++;
        }

        if (ability.CanUpgrade)
        {
            ability.Upgrade();
            Upgraded?.Invoke(ability);
        }

        Collected?.Invoke(ability);
    }

    public void SpawnAbility(Vector3 position)
    {
        List<AbilityPresenter> upgradableAbility = new List<AbilityPresenter>();

        if (_selectedAbilities.Count == 0)
        {
            upgradableAbility = _abilityPresenters.FindAll(ability => ability.AbilityInfo.Type == AbilityType.Active);
        }
        else
        {
            Func<AbilityPresenter, bool> activeCondition = ability => ability.AbilityInfo.Type == AbilityType.Active && ability.CanUpgrade;
            IEnumerable<AbilityPresenter> active = _aciveCount == MaxActiveAbilities ?
                _selectedAbilities.Where(activeCondition) :
                _abilityPresenters.Where(activeCondition);

            Func<AbilityPresenter, bool> passiveCondition = ability => ability.AbilityInfo.Type == AbilityType.Passive && ability.CanUpgrade;
            IEnumerable<AbilityPresenter> passive = _passiveCount == MaxPassiveAbilities ?
                _selectedAbilities.Where(passiveCondition) :
                _abilityPresenters.Where(passiveCondition);

            upgradableAbility = active.Concat(passive).ToList();
        }

        if (upgradableAbility.Count == 0)
            return;

        SpawnRandomAbility(upgradableAbility, position);
    }

    private void SpawnRandomAbility(List<AbilityPresenter> upgradableAbility, Vector3 position)
    {
        var randomIndex = UnityEngine.Random.Range(0, upgradableAbility.Count);
        AbilityPresenter abilitiesForSelect = upgradableAbility[randomIndex];

        var template = Instantiate(_template, position, Quaternion.identity);
        template.Init(this, abilitiesForSelect, transform);
        Spawned?.Invoke(template);
    }

#if UNITY_EDITOR
    [ContextMenu("Initialize")]
    private void Initialize()
    {
        _abilityPresenters?.Clear();
        _abilityPresenters = FindObjectsOfType<AbilityPresenter>(true).ToList();
        EditorUtility.SetDirty(gameObject);
    }
#endif
}
