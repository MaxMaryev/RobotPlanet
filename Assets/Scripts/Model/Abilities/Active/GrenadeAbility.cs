using System.Collections.Generic;

namespace BlobArena.Model
{
    public class GrenadeAbility : ActiveAbility<GrenadeAbility>
    {
        private const string GUID = "Grenade";
        private const string Name = "Grenade";
        private const string Description = "Explodes on hit";

        private readonly ProjectileSpeed _projectileSpeed = new ProjectileSpeed(10f);
        private readonly Radius _targetRadius = new Radius(0);
        private readonly Damage _targetDamage = new Damage(0);
        private readonly ProjectileCount _targetProjectileCount = new ProjectileCount(0);
        private IAbilityModification _modification;

        public GrenadeAbility(List<IAbilityListener<GrenadeAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.Grenade, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float>[]
                {
                    new Cooldown(4f),
                    new Cooldown(4f),
                    new Cooldown(3f),
                    new Cooldown(3f),
                    new Cooldown(2f),
                    new Cooldown(2f),
                    new Cooldown(2f),
                    new Cooldown(2f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(15),
                    new Damage(30),
                    new Damage(30),
                    new Damage(50),
                    new Damage(50),
                    new Damage(80),
                    new Damage(100),
                    new Damage(150),
                }),
                new FloatAbilityModification(_targetRadius, new IReadOnlyParam<float>[]
                {
                    new Radius(2.5f),
                    new Radius(2.5f),
                    new Radius(3.5f),
                    new Radius(3.5f),
                    new Radius(4f),
                    new Radius(4f),
                    new Radius(5f),
                    new Radius(5f),
                }),
                new IntAbilityModification(_targetProjectileCount, new IReadOnlyParam<int>[]
                {
                    new ProjectileCount(1),
                    new ProjectileCount(1),
                    new ProjectileCount(1),
                    new ProjectileCount(1),
                    new ProjectileCount(2),
                    new ProjectileCount(2),
                    new ProjectileCount(3),
                    new ProjectileCount(4),
                }),
            });
        }

        public float ProjectileSpeed => _projectileSpeed.Value;
        public int ProjectileCount => _targetProjectileCount.Value;
        public float Radius => _targetRadius.Value;
        public float Damage => _targetDamage.Value;
        protected override IAbilityModification Modification => _modification;
    }
}