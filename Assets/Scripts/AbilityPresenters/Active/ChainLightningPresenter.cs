using BlobArena.Model;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningPresenter : AbilityPresenter, IAbilityListener<ChainLightningAbility>, IUpdatable, IDamageBoostListener
{
    [SerializeField] private Lightning _template;
    [SerializeField] private EnemySpawner _enemySpawner;

    private ChainLightningAbility _ability;
    private float _damageModifier = 1f;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new ChainLightningAbility(new List<IAbilityListener<ChainLightningAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(ChainLightningAbility ability)
    {
        var lightning = Instantiate(_template, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        lightning.Init(_enemySpawner, ability.Speed, ability.Damage * _damageModifier, ability.JumpCount);
    }

    public void OnAbilityUpgrade(ChainLightningAbility ability) { }

    public void SetDamageModifier(float modifier)
    {
        _damageModifier = modifier;
    }
}
