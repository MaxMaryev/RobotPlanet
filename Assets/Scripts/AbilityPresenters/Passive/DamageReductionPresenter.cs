using BlobArena.Model;
using System.Collections.Generic;
using UnityEngine;

public class DamageReductionPresenter : AbilityPresenter, IAbilityListener<DamageReductionAbility>
{
    [SerializeField] private Player _player;

    private DamageReductionAbility _ability;

    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new DamageReductionAbility(new List<IAbilityListener<DamageReductionAbility>>() { this });
    }

    public void OnAbilityUpgrade(DamageReductionAbility ability)
    {
        _player.SetDamageReduction(ability.DamageReductionModifier);
    }

    public void OnAbilityUsed(DamageReductionAbility ability) { }
}
