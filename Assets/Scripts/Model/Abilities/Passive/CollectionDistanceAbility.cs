using System.Collections.Generic;

namespace BlobArena.Model
{
    public class CollectionDistanceAbility : PassiveAbility<CollectionDistanceAbility>
    {
        private const string GUID = "CollectionDistance";
        private const string Name = "Collection Distance";
        private const string Description = "Increases exp collection distance";

        private readonly IAbilityModification _modification;
        private readonly CollectionRadius _targetCollectionRadius = new CollectionRadius(0);

        public CollectionDistanceAbility(List<IAbilityListener<CollectionDistanceAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.CollectionDistance, listeners)
        {
            _modification = new PercentAbilityModification(_targetCollectionRadius,
                    new IReadOnlyParam<float>[]
                    {
                        new CollectionRadius(.05f),
                        new CollectionRadius(.1f),
                        new CollectionRadius(.15f),
                        new CollectionRadius(.2f),
                        new CollectionRadius(.25f),
                        new CollectionRadius(.3f),
                        new CollectionRadius(.4f),
                        new CollectionRadius(.5f),
                    });
        }

        public float CollectionRadiusModifier => 1f + _targetCollectionRadius.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
