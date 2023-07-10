using BlobArena.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocketPresenter : AbilityPresenter, IAbilityListener<HomingRocketAbility>, IUpdatable, IProjectCountListener, IDamageBoostListener
{
    private const float RocketDestroyTime = 5;

    [SerializeField] private RocketDrone _rocketDrone;
    [SerializeField] private Rocket _template;
    [SerializeField] private EnemySpawner _enemySpawner;

    private HomingRocketAbility _ability;
    private int _addingProjectCount = 0;
    private float _damageModifier = 1f;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new HomingRocketAbility(new List<IAbilityListener<HomingRocketAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(HomingRocketAbility ability)
    {
        StartCoroutine(CreateRockets(ability.RocketCount + _addingProjectCount, 0.2f));
    }

    public void OnAbilityUpgrade(HomingRocketAbility ability) 
    {
        if (_rocketDrone.gameObject.activeSelf == false)
            _rocketDrone.Enable();
    }

    private IEnumerator CreateRockets(int count, float delayBetweenSpawn)
    {
        for (int i = 0; i < count; i++)
        {
            Transform nearlyEnemy = _enemySpawner.GetNearlyEnemy(transform.position).Root;

            var rocket = Instantiate(_template, _rocketDrone.transform.position, Quaternion.identity);
            rocket.transform.LookAt(nearlyEnemy);
            rocket.Init(_ability.Damage * _damageModifier, _ability.RocketSpeed, RocketDestroyTime);

            yield return new WaitForSeconds(delayBetweenSpawn);
        }
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
