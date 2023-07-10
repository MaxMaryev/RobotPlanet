using System.Collections.Generic;

namespace BlobArena.Model
{
    public class ChainLightningAbility : ActiveAbility<ChainLightningAbility>
    {
        private const string GUID = "ChainLightning";
        private const string Name = "Chain Lightning";
        private const string Description = "Jump from target to target";

        private readonly ProjectileSpeed _speed = new ProjectileSpeed(10);
        private readonly Damage _targetDamage = new Damage(0);
        private readonly JumpCount _targetJumpCount = new JumpCount(0);
        private IAbilityModification _modification;

        public ChainLightningAbility(List<IAbilityListener<ChainLightningAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.ChainLightning, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float>[]
                {
                    new Cooldown(3f),
                    new Cooldown(2.5f),
                    new Cooldown(2.5f),
                    new Cooldown(2.5f),
                    new Cooldown(2f),
                    new Cooldown(2f),
                    new Cooldown(1.5f),
                    new Cooldown(1.5f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(8),
                    new Damage(25),
                    new Damage(25),
                    new Damage(50),
                    new Damage(90),
                    new Damage(125),
                    new Damage(150),
                    new Damage(200),
                }),
                new IntAbilityModification(_targetJumpCount, new IReadOnlyParam<int> []
                {
                    new ProjectileCount(2),
                    new ProjectileCount(2),
                    new ProjectileCount(3),
                    new ProjectileCount(3),
                    new ProjectileCount(3),
                    new ProjectileCount(5),
                    new ProjectileCount(6),
                    new ProjectileCount(8),
                }),
            });
        }

        public float Speed => _speed.Value;
        public float Damage => _targetDamage.Value;
        public int JumpCount => _targetJumpCount.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
