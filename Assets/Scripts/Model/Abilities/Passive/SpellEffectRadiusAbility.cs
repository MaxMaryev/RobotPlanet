using System.Collections.Generic;

namespace BlobArena.Model
{
    public class SpellEffectRadiusAbility : PassiveAbility<SpellEffectRadiusAbility>
    {
        private const string GUID = "SpellEffectRadius";
        private const string Name = "Spell Effect Radius";
        private const string Description = "Increases spell effect";

        private readonly IAbilityModification _modification;
        private readonly EffectRadius _targetEffectRadius = new EffectRadius(0);

        public SpellEffectRadiusAbility(List<IAbilityListener<SpellEffectRadiusAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.SpellEffectRadius, listeners)
        {
            _modification = new PercentAbilityModification(_targetEffectRadius,
                    new IReadOnlyParam<float>[]
                    {
                        new EffectRadius(.05f),
                        new EffectRadius(.1f),
                        new EffectRadius(.15f),
                        new EffectRadius(.2f),
                        new EffectRadius(.25f),
                        new EffectRadius(.4f),
                        new EffectRadius(.45f),
                        new EffectRadius(.5f),
                    });
        }

        public float SpellEffectRadiusModifier => 1f + _targetEffectRadius.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
