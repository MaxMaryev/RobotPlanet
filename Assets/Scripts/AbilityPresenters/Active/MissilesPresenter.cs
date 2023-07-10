using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;

public class MissilesPresenter : AbilityPresenter, IAbilityListener<MissilesAbility>, IUpdatable, IProjectCountListener, IDamageBoostListener
{
    private const float RocketDestroyTime = 5;

    [SerializeField] private Rocket _template;

    private MissilesAbility _ability;
    private int _addingProjectCount = 0;
    private float _damageModifier = 1f;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new MissilesAbility(new List<IAbilityListener<MissilesAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(MissilesAbility ability)
    {
        StartCoroutine(CreateRockets(ability.ProjectileCount + _addingProjectCount, 0.05f));
    }

    public void OnAbilityUpgrade(MissilesAbility ability) { }

    private IEnumerator CreateRockets(int count, float delayBetweenSpawn)
    {
        float angle = 2f * Mathf.PI / count;

        for (int i = 0; i < count; i++)
        {
            var direction = transform.position + new Vector3(Mathf.Cos(angle * (i + 1)), 0, Mathf.Sin(angle * (i + 1)));

            var rocket = Instantiate(_template, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            rocket.transform.LookAt(direction);
            rocket.transform.rotation = Quaternion.Euler(0, rocket.transform.rotation.eulerAngles.y, 0);
            rocket.Init(_ability.Damage * _damageModifier, _ability.ProjectileSpeed, RocketDestroyTime, _ability.PassThroughCount);

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
