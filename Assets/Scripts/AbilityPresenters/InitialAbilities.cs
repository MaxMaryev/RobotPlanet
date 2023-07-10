using BlobArena.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class InitialAbilities : MonoBehaviour
{
    public const int MaxCard = 3;
    public const int MaxPassiveAbilities = 4;
    public const int MaxActiveAbilities = 4;

    [SerializeField] private List<AbilityPresenter> _abilityPresenters;
    [SerializeField] private FlamethrowerPresenter _flamethrower;
   

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

   
    private void Start()
    {
        _selectedAbilities.Add(_flamethrower);
        _aciveCount++;
        Collected?.Invoke(_flamethrower);
        //SpawnAbility(transform.position + Vector3.forward * 5f);
    }

    private void Update()
    {
        foreach (var ability in _updatableAbilities)
            ability.Tick(Time.deltaTime);
    }

    //public void AddAbility(AbilityPresenter ability)
    //{
    //    if (ability.AbilityInfo.Level == 0)
    //    {
    //        _selectedAbilities.Add(ability);
           

    //        if (ability.AbilityInfo.Type == AbilityType.Active)
    //            _aciveCount++;
    //        else
    //            _passiveCount++;
    //    }

    //    if (ability.CanUpgrade)
    //    {
    //        ability.Upgrade();
    //        Upgraded?.Invoke(ability);
    //    }

    //    Collected?.Invoke(ability);
    //}

   

#if UNITY_EDITOR
    [ContextMenu("Initialize")]
    private void Initialize()
    {
        _abilityPresenters?.Clear();
        _abilityPresenters = FindObjectsOfType<AbilityPresenter>(true).ToList();
        //EditorUtility.SetDirty(gameObject);
    }
#endif
}

