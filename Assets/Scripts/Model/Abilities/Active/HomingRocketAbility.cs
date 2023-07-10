using System.Collections.Generic;

namespace BlobArena.Model
{
    public class HomingRocketAbility : ActiveAbility<HomingRocketAbility>
    {
        private const string GUID = "RocketDrone";
        private const string Name = "Rocket Drone";
        private const string Description = "Fly around and hits nearest enemies";

        private readonly Damage _targetDamage = new Damage(0);
        private readonly ProjectileSpeed _rocketSpeed = new ProjectileSpeed(10);
        private readonly ProjectileCount _targetProjectileCount = new ProjectileCount(0);
        private IAbilityModification _modification;

        public HomingRocketAbility(List<IAbilityListener<HomingRocketAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.HomingRocket, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float>[]
                {
                    new Cooldown(1.5f),
                    new Cooldown(1.5f),
                    new Cooldown(1.25f),
                    new Cooldown(1.25f),
                    new Cooldown(1f),
                    new Cooldown(1f),
                    new Cooldown(0.8f),
                    new Cooldown(0.75f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(10),
                    new Damage(10),
                    new Damage(25),
                    new Damage(50),
                    new Damage(100),
                    new Damage(175),
                    new Damage(250),
                    new Damage(300),
                }),
                new IntAbilityModification(_targetProjectileCount, new IReadOnlyParam<int> []
                {
                    new ProjectileCount(2),
                    new ProjectileCount(3),
                    new ProjectileCount(3),
                    new ProjectileCount(4),
                    new ProjectileCount(4),
                    new ProjectileCount(4),
                    new ProjectileCount(5),
                    new ProjectileCount(5),
                }),
            });
        }

        public float Damage => _targetDamage.Value;
        public int RocketCount => _targetProjectileCount.Value;
        public float RocketSpeed => _rocketSpeed.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
