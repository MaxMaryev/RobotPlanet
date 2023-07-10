using BlobArena.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPresenter : AbilityPresenter, IAbilityListener<TurretAbility>, IUpdatable, IProjectCountListener, IDamageBoostListener
{
    private const float TurretDestroyTime = 10;

    [SerializeField] private Turret _template;
    [SerializeField] private EnemySpawner _enemySpawner;

    private TurretAbility _ability;
    private float _damageModifier = 1f;
    private int _addingProjectCount = 0;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new TurretAbility(new List<IAbilityListener<TurretAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(TurretAbility ability)
    {
        StartCoroutine(CreateTurret(ability.ProjectileCount + _addingProjectCount, 0.2f));
    }

    public void OnAbilityUpgrade(TurretAbility ability) { }

    private IEnumerator CreateTurret(int rocketCount, float delayBetweenSpawn)
    {
        var position = RandomUtils.RandomInCirclePlane(0.5f, 3f);
        var turret = Instantiate(_template, transform.position + position, Quaternion.identity);
        turret.Init(_enemySpawner, _ability.Damage * _damageModifier, _ability.ProjectileSpeed, rocketCount, TurretDestroyTime);

        yield return new WaitForSeconds(delayBetweenSpawn);
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
