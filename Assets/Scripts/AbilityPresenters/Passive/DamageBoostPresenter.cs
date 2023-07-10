using BlobArena.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DamageBoostPresenter : AbilityPresenter, IAbilityListener<DamageBoostAbility>
{
    [SerializeField] private List<AbilityPresenter> _listenerPresenters;

    private DamageBoostAbility _ability;
    private List<IDamageBoostListener> _listeners = new List<IDamageBoostListener>();

    protected override IAbility Ability => _ability;

    private void OnValidate()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            if (_listeners[i] is IDamageBoostListener == false)
            {
                _listeners.RemoveAt(i);
                Debug.LogError("A non-listener removed from the list");
            }
        }
    }

    private void Awake()
    {
        _ability = new DamageBoostAbility(new List<IAbilityListener<DamageBoostAbility>>() { this });
        _listeners.AddRange(_listenerPresenters.Cast<IDamageBoostListener>());
    }

    public void OnAbilityUpgrade(DamageBoostAbility ability)
    {
        _listeners.ForEach(listener => listener.SetDamageModifier(ability.DamageModifier));
    }

    public void OnAbilityUsed(DamageBoostAbility ability) { }

#if UNITY_EDITOR
    [ContextMenu("Initialize")]
    private void Initialize()
    {
        _listenerPresenters?.Clear();
        _listenerPresenters = FindObjectsOfType<AbilityPresenter>(true).OfType<IDamageBoostListener>().Cast<AbilityPresenter>().ToList();
        EditorUtility.SetDirty(gameObject);
    }
#endif
}

public interface IDamageBoostListener
{
    void SetDamageModifier(float modifier);
}
