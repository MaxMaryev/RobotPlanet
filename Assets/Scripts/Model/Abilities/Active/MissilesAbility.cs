using System.Collections.Generic;

namespace BlobArena.Model
{
    public class MissilesAbility : ActiveAbility<MissilesAbility>
    {
        private const string GUID = "Missiles";
        private const string Name = "Missiles";
        private const string Description = "Shoots in all direction";

        private readonly PassThroughCount _targetPassThroughCount = new PassThroughCount(0);
        private readonly ProjectileSpeed _projectileSpeed = new ProjectileSpeed(10);
        private readonly ProjectileCount _targetProjectileCount = new ProjectileCount(0);
        private readonly Damage _targetDamage = new Damage(0);
        private IAbilityModification _modification;

        public MissilesAbility(List<IAbilityListener<MissilesAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.Missiles, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float>[]
                {
                    new Cooldown(2.5f),
                    new Cooldown(2f),
                    new Cooldown(2f),
                    new Cooldown(1.5f),
                    new Cooldown(1.5f),
                    new Cooldown(1.5f),
                    new Cooldown(1.25f),
                    new Cooldown(1f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(10),
                    new Damage(15),
                    new Damage(25),
                    new Damage(25),
                    new Damage(50),
                    new Damage(100),
                    new Damage(150),
                    new Damage(175),
                }),
                new IntAbilityModification(_targetPassThroughCount, new IReadOnlyParam<int>[]
                {
                    new PassThroughCount(2),
                    new PassThroughCount(2),
                    new PassThroughCount(4),
                    new PassThroughCount(4),
                    new PassThroughCount(6),
                    new PassThroughCount(6),
                    new PassThroughCount(7),
                    new PassThroughCount(7),
                }),
                new IntAbilityModification(_targetProjectileCount, new IReadOnlyParam<int>[]
                {
                    new ProjectileCount(3),
                    new ProjectileCount(3),
                    new ProjectileCount(3),
                    new ProjectileCount(4),
                    new ProjectileCount(4),
                    new ProjectileCount(7),
                    new ProjectileCount(7),
                    new ProjectileCount(9),
                }),
            });
        }

        public int PassThroughCount => _targetPassThroughCount.Value;
        public int ProjectileCount => _targetProjectileCount.Value;
        public float ProjectileSpeed => _projectileSpeed.Value;
        public float Damage => _targetDamage.Value;
        protected override IAbilityModification Modification => _modification;
    }
}