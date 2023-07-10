using System.Collections.Generic;

namespace BlobArena.Model
{
    public class BombFallAbility : ActiveAbility<BombFallAbility>
    {
        private const string GUID = "BombFall";
        private const string Name = "Bomb Fall";
        private const string Description = "Calls down a bomb anywhere on the screen";

        private readonly Cooldown _areaCooldown = new Cooldown(0.1f);
        private readonly DPS _targetDPS = new DPS(0);
        private readonly Radius _targetRadius = new Radius(0);
        private readonly Damage _targetDamage = new Damage(0);
        private IAbilityModification _modification;

        public BombFallAbility(List<IAbilityListener<BombFallAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.BombFall, listeners)
        {
            _modification = new AbilityModificationList(new IAbilityModification[]
            {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float>[]
                {
                    new Cooldown(6.5f),
                    new Cooldown(6.5f),
                    new Cooldown(5f),
                    new Cooldown(5f),
                    new Cooldown(4f),
                    new Cooldown(4f),
                    new Cooldown(3f),
                    new Cooldown(3f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(25),
                    new Damage(25),
                    new Damage(50),
                    new Damage(50),
                    new Damage(125),
                    new Damage(125),
                    new Damage(200),
                    new Damage(300),
                }),
                new FloatAbilityModification(_targetRadius, new IReadOnlyParam<float>[]
                {
                    new Radius(3f),
                    new Radius(4f),
                    new Radius(4f),
                    new Radius(5f),
                    new Radius(5f),
                    new Radius(5f),
                    new Radius(6f),
                    new Radius(6f),
                }),
                new FloatAbilityModification(_targetDPS, new IReadOnlyParam<float>[]
                {
                    new DPS(15f),
                    new DPS(20f),
                    new DPS(20f),
                    new DPS(30f),
                    new DPS(40f),
                    new DPS(70f),
                    new DPS(70f),
                    new DPS(100f),
                }),
            });
        }

        public float Radius => _targetRadius.Value;
        public float FallDamage => _targetDamage.Value;
        public float AreaDamage => _targetDPS.Value * _areaCooldown.Value;
        public float AreaCooldown => _areaCooldown.Value;
        protected override IAbilityModification Modification => _modification;
    }
}