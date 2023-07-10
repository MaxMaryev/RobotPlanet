using System.Collections.Generic;

namespace BlobArena.Model
{
    public class DamageReductionAbility : PassiveAbility<DamageReductionAbility>
    {
        private const string GUID = "DamageReduction";
        private const string Name = "Damage Reduction";
        private const string Description = "Provides damage reduction";

        private readonly IAbilityModification _modification;
        private readonly DamageReduction _targetDamageReduction = new DamageReduction(0);

        public DamageReductionAbility(List<IAbilityListener<DamageReductionAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.DamageReduction, listeners)
        {
            _modification = new PercentAbilityModification(_targetDamageReduction,
                    new IReadOnlyParam<float>[]
                    {
                        new DamageReduction(.05f),
                        new DamageReduction(.1f),
                        new DamageReduction(.15f),
                        new DamageReduction(.2f),
                        new DamageReduction(.25f),
                        new DamageReduction(.3f),
                        new DamageReduction(.45f),
                        new DamageReduction(.5f),
                    });
        }

        public float DamageReductionModifier => 1f - _targetDamageReduction.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
