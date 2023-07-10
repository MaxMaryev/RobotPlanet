using System.Collections.Generic;

namespace BlobArena.Model
{
    public class FlamethrowerAbility : ActiveAbility<FlamethrowerAbility>
    {
        private const string GUID = "Flamethrower";
        private const string Name = "Flamethrower";
        private const string Description = "Burns enemies";

        private readonly Radius _targetRadius = new Radius(0);
        private readonly Damage _targetDamage = new Damage(0);
        private IAbilityModification _modification;

        public FlamethrowerAbility(List<IAbilityListener<FlamethrowerAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.Flamethrower, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float>[]
                {
                    new Cooldown(3f),
                    new Cooldown(3f),
                    new Cooldown(2.5f),
                    new Cooldown(2.5f),
                    new Cooldown(2f),
                    new Cooldown(1.5f),
                    new Cooldown(1f),
                    new Cooldown(1f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(3),
                    new Damage(6),
                    new Damage(10),
                    new Damage(12),
                    new Damage(12),
                    new Damage(21),
                    new Damage(36),
                    new Damage(60),
                }),
                new FloatAbilityModification(_targetRadius, new IReadOnlyParam<float>[]
                {
                    new Radius(1.5f),
                    new Radius(1.5f),
                    new Radius(1.5f),
                    new Radius(2.5f),
                    new Radius(3f),
                    new Radius(3f),
                    new Radius(3f),
                    new Radius(3f),
                }),
            });
        }

        public float Radius => _targetRadius.Value;
        public float Damage => _targetDamage.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
