using System.Collections.Generic;

namespace BlobArena.Model
{
    public class ProjectileCountAbillity : PassiveAbility<ProjectileCountAbillity>
    {
        private const string GUID = "ProjectileCount";
        private const string Name = "Projectile Count";
        private const string Description = "Increases skill projectile counts";

        private readonly IAbilityModification _modification;
        private readonly ProjectileCount _targetProjectileCount = new ProjectileCount(0);

        public ProjectileCountAbillity(List<IAbilityListener<ProjectileCountAbillity>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.ProjectileCount, listeners)
        {
            _modification = new IntAbilityModification(_targetProjectileCount,
                    new IReadOnlyParam<int>[]
                    {
                        new ProjectileCount(1),
                        new ProjectileCount(2),
                    });
        }

        public int AddingProjectileCount => _targetProjectileCount.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
