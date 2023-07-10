using System.Collections.Generic;

namespace BlobArena.Model
{
    public class DamageBoostAbility : PassiveAbility<DamageBoostAbility>
    {
        private const string GUID = "DamageBoost";
        private const string Name = "Damage Boost";
        private const string Description = "Increases skill damage";

        private readonly IAbilityModification _modification;
        private readonly DamagePercent _targetDamage = new DamagePercent(0);

        public DamageBoostAbility(List<IAbilityListener<DamageBoostAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.DamageBoost, listeners)
        {
            _modification = new PercentAbilityModification(_targetDamage,
                    new IReadOnlyParam<float>[]
                    {
                        new DamagePercent(.1f),
                        new DamagePercent(.2f),
                        new DamagePercent(.3f),
                        new DamagePercent(.4f),
                        new DamagePercent(.5f),
                        new DamagePercent(.65f),
                        new DamagePercent(.8f),
                        new DamagePercent(1.0f),
                    });
        }

        public float DamageModifier => 1f + _targetDamage.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
