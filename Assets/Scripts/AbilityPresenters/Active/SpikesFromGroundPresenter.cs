using System.Collections.Generic;
using UnityEngine;
using BlobArena.Model;
using System.Collections;

public class SpikesFromGroundPresenter : AbilityPresenter, IAbilityListener<SpikeAbility>, IUpdatable, IProjectCountListener, IDamageBoostListener
{
    [SerializeField] private SpikesFromGround _spikerTemplate;
    private SpikeAbility _ability;
    private float _damageModifier = 1f;
    private int _addingProjectCount = 0;

    public ITimer Timer => _ability.Timer;
    public override bool HasTimer => true;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new SpikeAbility(new List<IAbilityListener<SpikeAbility>>() { this });
    }

    public void Tick(float tick)
    {
        _ability.Tick(tick);
    }

    public void OnAbilityUsed(SpikeAbility ability)
    {
        int SpikeCount = ability.SpikeCount + _addingProjectCount;
        StartCoroutine(CreateSpike(SpikeCount, 1f));
    }

    public void OnAbilityUpgrade(SpikeAbility ability) { }

    private IEnumerator CreateSpike(int count, float delayBetweenSpawn)
    {
        for (int i = 0; i < count; i++)
        {
            var randomOffset = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 10f));
            var spawnPosition = transform.position + Vector3.down * 5f + randomOffset;
            var spawnedSpiker = Instantiate(_spikerTemplate, spawnPosition, Quaternion.identity);
            spawnedSpiker.Init(_ability.Damage * _damageModifier, _ability.AreaCooldown, 2f);
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
