using System.Collections.Generic;

namespace BlobArena.Model
{
    public class OrbsAbility : ActiveAbility<OrbsAbility>
    {
        private const string GUID = "Orbs";
        private const string Name = "Orbital drons";
        private const string Description = "Rotates around the hero";

        private readonly Radius _targetRadius = new Radius(0);
        private readonly Damage _targetDamage = new Damage(0);
        private readonly ProjectileCount _targetProjectileCount = new ProjectileCount(0);
        private readonly ProjectileSpeed _targetProjectileSpeed = new ProjectileSpeed(0);
        private IAbilityModification _modification;

        public OrbsAbility(List<IAbilityListener<OrbsAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.Orbs, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(_targetRadius, new IReadOnlyParam<float> []
                {
                    new Radius(1.25f),
                    new Radius(1.25f),
                    new Radius(1.25f),
                    new Radius(1.5f),
                    new Radius(1.5f),
                    new Radius(1.5f),
                    new Radius(2f),
                    new Radius(2f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(5),
                    new Damage(10),
                    new Damage(30),
                    new Damage(30),
                    new Damage(75),
                    new Damage(100),
                    new Damage(150),
                    new Damage(250),
                }),
                new FloatAbilityModification(_targetProjectileSpeed, new IReadOnlyParam<float>[]
                {
                    new ProjectileSpeed(17f),
                    new ProjectileSpeed(17f),
                    new ProjectileSpeed(20f),
                    new ProjectileSpeed(20f),
                    new ProjectileSpeed(20f),
                    new ProjectileSpeed(20f),
                    new ProjectileSpeed(20f),
                    new ProjectileSpeed(25),
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

        public float Radius => _targetRadius.Value;
        public float Damage => _targetDamage.Value;
        public int OrbsCount => _targetProjectileCount.Value;
        public float OrbsSpeed => _targetProjectileSpeed.Value * 10;
        protected override IAbilityModification Modification => _modification;
    }
}
