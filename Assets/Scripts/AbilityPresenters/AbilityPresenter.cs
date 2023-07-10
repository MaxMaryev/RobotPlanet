using UnityEngine;
using UnityEngine.Events;
using BlobArena.Model;
using System;

public abstract class AbilityPresenter : MonoBehaviour
{
    public bool CanUpgrade => Ability.CanUpgrade;
    public AbilityInfo AbilityInfo => Ability.Info;
    public virtual bool HasTimer { get; } = false;
    protected abstract IAbility Ability { get; }

    public event UnityAction<AbilityPresenter> Upgraded;

    public void Upgrade()
    {
        if (CanUpgrade == false)
            throw new InvalidOperationException();

        Ability.Upgrade();
        Upgraded?.Invoke(this);
    }
}