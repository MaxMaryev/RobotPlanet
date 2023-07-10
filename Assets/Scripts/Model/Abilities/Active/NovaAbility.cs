using System.Collections.Generic;

namespace BlobArena.Model
{
    public class NovaAbility : ActiveAbility<NovaAbility>
    {
        private const string GUID = "Nova";
        private const string Name = "Nova";
        private const string Description = "Damages nearby enemies";

        private readonly Radius _targetRadius = new Radius(0);
        private readonly Damage _targetDamage = new Damage(0);
        private IAbilityModification _modification;

        public NovaAbility(List<IAbilityListener<NovaAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.Nova, listeners)
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
                    new Cooldown(1.5f),
                    new Cooldown(1f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(2),
                    new Damage(5),
                    new Damage(10),
                    new Damage(10),
                    new Damage(25),
                    new Damage(25),
                    new Damage(30),
                    new Damage(50),
                }),
                new FloatAbilityModification(_targetRadius, new IReadOnlyParam<float>[]
                {
                    new Radius(1.5f),
                    new Radius(1.5f),
                    new Radius(1.5f),
                    new Radius(2.5f),
                    new Radius(2.5f),
                    new Radius(2.5f),
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