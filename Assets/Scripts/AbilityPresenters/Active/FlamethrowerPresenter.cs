using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;
using System.Collections;

public class FlamethrowerPresenter : AbilityPresenter, IAbilityListener<FlamethrowerAbility>, IUpdatable, ISpellEffectRadiusListener, IDamageBoostListener
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Transform _player;
    [SerializeField] private AbilityTrigger _trigger;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private Light _light;

    private FlamethrowerAbility _ability;
    private float _radiusModifier = 1f;
    private float _damageModifier = 1f;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new FlamethrowerAbility(new List<IAbilityListener<FlamethrowerAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(FlamethrowerAbility ability)
    {
        _effect.Play();
        _light.enabled = true;
        StartCoroutine(Use(ability, 0.5f));
    }

    public void OnAbilityUpgrade(FlamethrowerAbility ability)
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

    private IEnumerator Aim()
    {
        bool isShooting = true;
        transform.LookAt(_enemySpawner.GetNearlyEnemy(transform.position).Root);

        while (isShooting)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-_enemySpawner.GetNearlyEnemy(transform.position).Root.forward), Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Use(FlamethrowerAbility ability, float duration)
    {
        float startTime = Time.time;
        Coroutine aim = StartCoroutine(Aim());

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
            {
                if (aim != null)
                    StopCoroutine(aim);

                _light.enabled = false;
                _effect.Stop();
                break;
            }
        }
    }
}
