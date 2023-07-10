using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;
using System.Collections;

public class NovaPresenter : AbilityPresenter, IAbilityListener<NovaAbility>, IUpdatable, ISpellEffectRadiusListener, IDamageBoostListener
{
    [SerializeField] private AbilityTrigger _trigger;
    [SerializeField] private ParticleSystem _effect;

    private NovaAbility _ability;
    private float _radiusModifier = 1f;
    private float _damageModifier = 1f;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new NovaAbility(new List<IAbilityListener<NovaAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(NovaAbility ability)
    {
        _effect.Play();
        StartCoroutine(Use(ability, 0.5f));
    }

    public void OnAbilityUpgrade(NovaAbility ability) 
    {
        transform.localScale = new Vector3(ability.Radius * _radiusModifier, transform.localScale.y, ability.Radius * _radiusModifier);
    }

    public void SetRadiusModifier(float modifier)
    {
        _radiusModifier = modifier;
    }

    public void SetDamageModifier(float modifier)
    {
        _damageModifier = modifier;
    }

    private IEnumerator Use(NovaAbility ability, float duration)
    {
        float startTime = Time.time;
        while (true)
        {
            var enemies = _trigger.EnteredEnemies;

            foreach (var enemy in enemies)
            {
                if (enemy == null)
                    continue;

                enemy.TakeDamage(ability.Damage * _damageModifier);
            }

            yield return new WaitForSeconds(duration / 5f);
            
            if (Time.time - startTime > duration)
                break;
        }
    }
}
