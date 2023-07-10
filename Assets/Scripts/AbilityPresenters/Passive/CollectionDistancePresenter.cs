using BlobArena.Model;
using System.Collections.Generic;
using UnityEngine;

public class CollectionDistancePresenter : AbilityPresenter, IAbilityListener<CollectionDistanceAbility>
{
    [SerializeField] private ExperienceCollector _experienceCollector;

    private CollectionDistanceAbility _ability;

    protected override IAbility Ability => _ability;

    private void Awake()
    {
        _ability = new CollectionDistanceAbility(new List<IAbilityListener<CollectionDistanceAbility>>() { this });
    }

    public void OnAbilityUpgrade(CollectionDistanceAbility ability)
    {
        //_experienceCollector.transform.localScale *= ability.CollectionRadiusModifier;
    }

    public void OnAbilityUsed(CollectionDistanceAbility ability) { }
}
