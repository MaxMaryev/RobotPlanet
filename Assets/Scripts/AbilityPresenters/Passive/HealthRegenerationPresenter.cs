using BlobArena.Model;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenerationPresenter : AbilityPresenter, IAbilityListener<HealthRegenerationAbility>, IUpdatable
{
    [SerializeField] private Player _player;

    private HealthRegenerationAbility _ability;

    public ITimer Timer => _ability.Timer;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new HealthRegenerationAbility(new List<IAbilityListener<HealthRegenerationAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(HealthRegenerationAbility ability) 
    {
        _player.Regenerate(ability.HealthRegenerationModifier);
    }

    public void OnAbilityUpgrade(HealthRegenerationAbility ability) { }
}
