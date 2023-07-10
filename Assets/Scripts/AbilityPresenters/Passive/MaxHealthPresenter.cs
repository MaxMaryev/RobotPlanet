using UnityEngine;
using System.Collections.Generic;
using BlobArena.Model;

public class MaxHealthPresenter : AbilityPresenter, IAbilityListener<MaxHealthAbility>
{
    [SerializeField] private Player _player;

    private MaxHealthAbility _ability;

    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new MaxHealthAbility(new List<IAbilityListener<MaxHealthAbility>>() { this });
    }

    public void OnAbilityUpgrade(MaxHealthAbility ability)
    {
        _player.SetHealthModifier(ability.MaxHealthModifier);
    }

    public void OnAbilityUsed(MaxHealthAbility ability) { }
}
