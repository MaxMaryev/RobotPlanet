using BlobArena.Model;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedPresenter : AbilityPresenter, IAbilityListener<MovementSpeedAbility>
{
    [SerializeField] private PlayerMovement _playerMovement;

    private MovementSpeedAbility _ability;

    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new MovementSpeedAbility(new List<IAbilityListener<MovementSpeedAbility>>() { this });
    }

    public void OnAbilityUpgrade(MovementSpeedAbility ability)
    {
        _playerMovement.SetSpeedModifier(ability.MovementSpeedModifier);
    }

    public void OnAbilityUsed(MovementSpeedAbility ability) { }
}
