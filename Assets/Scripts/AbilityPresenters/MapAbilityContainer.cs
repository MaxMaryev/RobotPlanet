using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapAbilityContainer : MonoBehaviour
{
    [SerializeField] private Abilities _abilities;

    private List<AbilityCardPresneter> _presenters = new List<AbilityCardPresneter>();

    public event UnityAction<AbilityCardPresneter> Added;
    public event UnityAction<AbilityCardPresneter> Removed;

    private void OnEnable()
    {
        _abilities.Spawned += OnAbilitySpawned;
    }

    private void OnDisable()
    {
        _abilities.Spawned -= OnAbilitySpawned;
        _presenters.ForEach(presenter => presenter.Destroyed -= OnCardDestroyed);
    }

    private void OnAbilitySpawned(AbilityCardPresneter presenter)
    {
        presenter.Destroyed += OnCardDestroyed;
        _presenters.Add(presenter);

        Added?.Invoke(presenter);
    }

    private void OnCardDestroyed(AbilityCardPresneter presenter)
    {
        presenter.Destroyed -= OnCardDestroyed;
        _presenters.Remove(presenter);
        Removed?.Invoke(presenter);
    }
}
