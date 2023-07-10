using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;

public class GrenadePresenter : AbilityPresenter, IAbilityListener<GrenadeAbility>, IUpdatable, ISpellEffectRadiusListener, IProjectCountListener, IDamageBoostListener
{
    private const float GrenadeDestroyTime = 5;

    [SerializeField] private Grenade _template;
    [SerializeField] private EnemySpawner _enemySpawner;

    private GrenadeAbility _ability;
    private float _radiusModifier = 1f;
    private float _damageModifier = 1f;
    private int _addingProjectCount = 0;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new GrenadeAbility(new List<IAbilityListener<GrenadeAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(GrenadeAbility ability)
    {
        StartCoroutine(CreateGrenade(ability.ProjectileCount + _addingProjectCount, 0.2f));
    }

    public void OnAbilityUpgrade(GrenadeAbility ability) { }

    private IEnumerator CreateGrenade(int count, float delayBetweenSpawn)
    {
        for (int i = 0; i < count; i++)
        {
            Transform nearlyEnemy = _enemySpawner.GetNearlyEnemy(transform.position).Root;

            var grenade = Instantiate(_template, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            grenade.transform.LookAt(nearlyEnemy);
            grenade.transform.rotation = Quaternion.Euler(0, grenade.transform.rotation.eulerAngles.y, 0);
            grenade.Init(_ability.Damage * _damageModifier, _ability.ProjectileSpeed, _ability.Radius * _radiusModifier, GrenadeDestroyTime);

            yield return new WaitForSeconds(delayBetweenSpawn);
        }
    }

    public void SetRadiusModifier(float modifier)
    {
        _radiusModifier = modifier;
    }

    public void SetAddingProjectCount(int count)
    {
        _addingProjectCount = count;
    }

    public void SetDamageModifier(float modifier)
    {
        _damageModifier = modifier;
    }
}
