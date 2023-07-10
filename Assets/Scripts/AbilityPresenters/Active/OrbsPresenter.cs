using BlobArena.Model;
using System.Collections.Generic;
using UnityEngine;

public class OrbsPresenter : AbilityPresenter, IAbilityListener<OrbsAbility>, IUpdatable, ISpellEffectRadiusListener, IProjectCountListener, IDamageBoostListener
{
    [SerializeField] private Orb _orbTemplate;

    private float _rotationSpeed;
    private float _radiusModifier = 1f;
    private float _damageModifier = 1f;
    private int _addingProjectCount = 0;
    private OrbsAbility _ability;
    private List<Orb> _spawnedOrbs = new List<Orb>();

    public ITimer Timer => _ability.Timer;
    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new OrbsAbility(new List<IAbilityListener<OrbsAbility>>() { this });
    }

    public void Tick(float tick)
    {
        transform.Rotate(Vector3.down * _rotationSpeed * tick, Space.World);
        _ability.Tick(tick);
    }

    public void OnAbilityUpgrade(OrbsAbility ability)
    {
        int orbsCount = ability.OrbsCount > 0 ? ability.OrbsCount + _addingProjectCount : 0;

        if (orbsCount > _spawnedOrbs.Count)
        {
            for (int i = 0; i < orbsCount - _spawnedOrbs.Count; i++)
            {
                _spawnedOrbs.Add(Instantiate(_orbTemplate, transform));
            }
        }

        _rotationSpeed = ability.OrbsSpeed;
        float angle = 2f * Mathf.PI / orbsCount;

        for (int i = 0; i < _spawnedOrbs.Count; i++)
        {
            _spawnedOrbs[i].SetDamage(ability.Damage * _damageModifier);
            _spawnedOrbs[i].transform.localPosition = new Vector3(ability.Radius * _radiusModifier * Mathf.Cos(angle * (i+1)), 0, 
                                                                  ability.Radius * _radiusModifier * Mathf.Sin(angle * (i+1)));
        }
    }

    public void OnAbilityUsed(OrbsAbility ability) { }

    public void SetRadiusModifier(float modifier)
    {
        _radiusModifier = modifier;
        OnAbilityUpgrade(_ability);
    }

    public void SetAddingProjectCount(int count)
    {
        _addingProjectCount = count;
        OnAbilityUpgrade(_ability);
    }

    public void SetDamageModifier(float modifier)
    {
        _damageModifier = modifier;
        OnAbilityUpgrade(_ability);
    }
}
