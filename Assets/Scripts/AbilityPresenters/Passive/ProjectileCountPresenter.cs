using BlobArena.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ProjectileCountPresenter : AbilityPresenter, IAbilityListener<ProjectileCountAbillity>
{
    [SerializeField] private List<AbilityPresenter> _listenerPresenters;

    private ProjectileCountAbillity _ability;
    private List<IProjectCountListener> _listeners = new List<IProjectCountListener>();

    protected override IAbility Ability => _ability;

    private void OnValidate()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            if (_listeners[i] is IProjectCountListener == false)
            {
                _listeners.RemoveAt(i);
                Debug.LogError("A non-listener removed from the list");
            }
        }
    }

    private void Awake()
    {
        _ability = new ProjectileCountAbillity(new List<IAbilityListener<ProjectileCountAbillity>>() { this });
        _listeners.AddRange(_listenerPresenters.Cast<IProjectCountListener>());
    }

    public void OnAbilityUpgrade(ProjectileCountAbillity ability)
    {
        _listeners.ForEach(listener => listener.SetAddingProjectCount(ability.AddingProjectileCount));
    }

    public void OnAbilityUsed(ProjectileCountAbillity ability) { }

#if UNITY_EDITOR
    [ContextMenu("Initialize")]
    private void Initialize()
    {
        _listenerPresenters?.Clear();
        _listenerPresenters = FindObjectsOfType<AbilityPresenter>(true).OfType<IProjectCountListener>().Cast<AbilityPresenter>().ToList();
        EditorUtility.SetDirty(gameObject);
    }
#endif
}

public interface IProjectCountListener
{
    void SetAddingProjectCount(int count);
}
