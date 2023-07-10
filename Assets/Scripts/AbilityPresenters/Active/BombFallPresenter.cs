using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;

public class BombFallPresenter : AbilityPresenter, IAbilityListener<BombFallAbility>, IUpdatable, ISpellEffectRadiusListener, IDamageBoostListener
{
    [SerializeField] private FallingBomb _bombTemplate;
    [SerializeField] private BombDamageArea _damageAreaTemplate;

    private BombFallAbility _ability;
    private float _radiusModifier = 1f;
    private float _damageModifier = 1f;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new BombFallAbility(new List<IAbilityListener<BombFallAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(BombFallAbility ability)
    {
        var randomOffset = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 10f));
        var spawnPosition = transform.position + Vector3.up * 5f + randomOffset;
        var targetPosition = new Vector3(spawnPosition.x, 1, spawnPosition.z);

        var spawnedBomb = Instantiate(_bombTemplate, spawnPosition, Quaternion.identity);
        spawnedBomb.Init(targetPosition, ability.FallDamage * _damageModifier, 0.5f).OnFall(() => 
        {
            var damageArea = Instantiate(_damageAreaTemplate, targetPosition, _damageAreaTemplate.transform.rotation);
            damageArea.Init(ability.Radius * _radiusModifier, ability.AreaDamage, ability.AreaCooldown, 6f);
        });
    }

    public void OnAbilityUpgrade(BombFallAbility ability) { }

    public void SetRadiusModifier(float modifier)
    {
        _radiusModifier = modifier;
    }

    public void SetDamageModifier(float modifier)
    {
        _damageModifier = modifier;
    }
}
