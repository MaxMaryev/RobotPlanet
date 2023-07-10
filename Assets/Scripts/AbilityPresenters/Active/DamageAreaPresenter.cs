using UnityEngine;
using BlobArena.Model;
using System.Collections.Generic;

public class DamageAreaPresenter : AbilityPresenter, IAbilityListener<DamageAreaAbility>, IUpdatable, ISpellEffectRadiusListener
{
    [SerializeField] private AbilityTrigger _trigger;

    private DamageAreaAbility _ability;
    private float _radiusModifier = 1f;

    public ITimer Timer => _ability.Timer;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new DamageAreaAbility(new List<IAbilityListener<DamageAreaAbility>>() { this });
        gameObject.SetActive(false);
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(DamageAreaAbility ability)
    {
        var enemies = _trigger.EnteredEnemies;

        foreach (var enemy in enemies)
        {
            if (enemy == null)
                continue;

            enemy.TakeDamage(ability.Damage);
        }
    }

    public void OnAbilityUpgrade(DamageAreaAbility ability)
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);

        transform.localScale = new Vector3(ability.Radius * _radiusModifier, transform.localScale.y, ability.Radius * _radiusModifier);
    }

    public void SetRadiusModifier(float modifier)
    {
        _radiusModifier = modifier;

        if (gameObject.activeSelf)
            OnAbilityUpgrade(_ability);
    }
}
