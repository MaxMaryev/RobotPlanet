using System.Collections.Generic;

namespace BlobArena.Model
{
    public class SpikeAbility : ActiveAbility<SpikeAbility>
    {

        private const string GUID = "Spike";
        private const string Name = "Spikes";
        private const string Description = "Spikes coming out of the ground";

        private readonly Cooldown _areaCooldown = new Cooldown(0.1f);
        private readonly ProjectileCount _targetProjectileCount = new ProjectileCount(0);
        private readonly Damage _targetDamage = new Damage(0);
        private IAbilityModification _modification;

        public SpikeAbility(List<IAbilityListener<SpikeAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.Spike, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float>[]
                {
                    new Cooldown(7f),
                    new Cooldown(7f),
                    new Cooldown(6.5f),
                    new Cooldown(6.5f),
                    new Cooldown(5f),
                    new Cooldown(5f),
                    new Cooldown(4f),
                    new Cooldown(3f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(45),
                    new Damage(45),
                    new Damage(70),
                    new Damage(70),
                    new Damage(120),
                    new Damage(150),
                    new Damage(300),
                    new Damage(400),
                }),
                new IntAbilityModification(_targetProjectileCount, new IReadOnlyParam<int> []
                {
                    new ProjectileCount(1),
                    new ProjectileCount(2),
                    new ProjectileCount(2),
                    new ProjectileCount(3),
                    new ProjectileCount(3),
                    new ProjectileCount(4),
                    new ProjectileCount(4),
                    new ProjectileCount(5),
                }),
                });
        }
        public float Damage => _targetDamage.Value;
        public int SpikeCount => _targetProjectileCount.Value;
        public float AreaCooldown => _areaCooldown.Value;
        protected override IAbilityModification Modification => _modification;

    }
}
