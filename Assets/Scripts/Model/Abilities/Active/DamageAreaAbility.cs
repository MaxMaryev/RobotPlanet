using System.Collections.Generic;

namespace BlobArena.Model
{
    public class DamageAreaAbility : ActiveAbility<DamageAreaAbility>
    {
        private const string GUID = "DamageArea";
        private const string Name = "Damage area";
        private const string Description = "Continuous damage to nearby enemies";

        private readonly DPS _targetDPS = new DPS(0);
        private readonly Radius _targetRadius = new Radius(0);
        private IAbilityModification _modification;

        public DamageAreaAbility(List<IAbilityListener<DamageAreaAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.DamageArea, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float> []
                {
                    new Cooldown(0.1f)
                }),
                new FloatAbilityModification(_targetDPS, new IReadOnlyParam<float> []
                {
                    new DPS(5f),
                    new DPS(15f),
                    new DPS(25f),
                    new DPS(25f),
                    new DPS(50f),
                    new DPS(100f),
                    new DPS(150f),
                    new DPS(200f),
                }),
                new FloatAbilityModification(_targetRadius, new IReadOnlyParam<float> []
                {
                    new Radius(1.25f),
                    new Radius(1.5f),
                    new Radius(1.5f),
                    new Radius(1.75f),
                    new Radius(1.75f),
                    new Radius(1.82f),
                    new Radius(1.82f),
                    new Radius(2f),
                }),
            });
        }

        public float Damage => _targetDPS.Value * TargetCooldown.Value;
        public float Radius => _targetRadius.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
