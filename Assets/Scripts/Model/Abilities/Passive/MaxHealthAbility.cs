using System.Collections.Generic;

namespace BlobArena.Model
{
    public class MaxHealthAbility : PassiveAbility<MaxHealthAbility>
    {
        private const string GUID = "MaxHealth";
        private const string Name = "Max Health";
        private const string Description = "Increase max hp";

        private readonly IAbilityModification _modification;
        private readonly MaxHealth _targetMaxHealth = new MaxHealth(0);

        public MaxHealthAbility(List<IAbilityListener<MaxHealthAbility>> listeners = null) 
            : base(GUID, Name, Description, AbilityIdentifier.MaxHealth, listeners) 
        {
            _modification = new PercentAbilityModification(_targetMaxHealth,
                    new IReadOnlyParam<float>[]
                    {
                        new MaxHealth(.05f),
                        new MaxHealth(.1f),
                        new MaxHealth(.15f),
                        new MaxHealth(.2f),
                        new MaxHealth(.25f),
                        new MaxHealth(.3f),
                        new MaxHealth(.45f),
                        new MaxHealth(.5f),
                    });
        }

        public float MaxHealthModifier => 1f + _targetMaxHealth.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
