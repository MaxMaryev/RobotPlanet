using BlobArena.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpellEffectRadiusPresenter : AbilityPresenter, IAbilityListener<SpellEffectRadiusAbility>
{
    [SerializeField] private List<AbilityPresenter> _listenerPresenters;

    private SpellEffectRadiusAbility _ability;
    private List<ISpellEffectRadiusListener> _listeners = new List<ISpellEffectRadiusListener>();

    protected override IAbility Ability => _ability;

    private void OnValidate()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            if (_listeners[i] is ISpellEffectRadiusListener == false)
            {
                _listeners.RemoveAt(i);
                Debug.LogError("A non-listener removed from the list");
            }
        }
    }

    private void Awake()
    {
        _ability = new SpellEffectRadiusAbility(new List<IAbilityListener<SpellEffectRadiusAbility>>() { this });
        _listeners.AddRange(_listenerPresenters.Cast<ISpellEffectRadiusListener>());
    }

    public void OnAbilityUpgrade(SpellEffectRadiusAbility ability)
    {
        _listeners.ForEach(listener => listener.SetRadiusModifier(ability.SpellEffectRadiusModifier));
    }

    public void OnAbilityUsed(SpellEffectRadiusAbility ability) { }

#if UNITY_EDITOR
    [ContextMenu("Initialize")]
    private void Initialize()
    {
        _listenerPresenters?.Clear();
        _listenerPresenters = FindObjectsOfType<AbilityPresenter>(true).OfType<ISpellEffectRadiusListener>().Cast<AbilityPresenter>().ToList();
        EditorUtility.SetDirty(gameObject);
    }
#endif
}

public interface ISpellEffectRadiusListener
{
    void SetRadiusModifier(float modifier);
}
