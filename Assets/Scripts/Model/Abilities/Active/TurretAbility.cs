using System.Collections.Generic;
using BlobArena.Model;

public class TurretAbility : ActiveAbility<TurretAbility>
{
    private const string GUID = "Turret";
    private const string Name = "Turret";
    private const string Description = "Creates a turret that fires at enemies";

    private readonly ProjectileSpeed _projectileSpeed = new ProjectileSpeed(20f);
    private readonly Damage _targetDamage = new Damage(0);
    private readonly ProjectileCount _targetProjectileCount = new ProjectileCount(0);
    private IAbilityModification _modification;

    public TurretAbility(List<IAbilityListener<TurretAbility>> listeners = null)
        : base(GUID, Name, Description, AbilityIdentifier.Turret, listeners)
    {
        _modification = new AbilityModificationList(new IAbilityModification[]
        {
                new FloatAbilityModification(TargetCooldown, new IReadOnlyParam<float>[]
                {
                    new Cooldown(7f),
                    new Cooldown(7f),
                    new Cooldown(6f),
                    new Cooldown(6f),
                    new Cooldown(5f),
                    new Cooldown(5f),
                    new Cooldown(4f),
                    new Cooldown(4f),
                }),
                new FloatAbilityModification(_targetDamage, new IReadOnlyParam<float>[]
                {
                    new Damage(2),
                    new Damage(10),
                    new Damage(20),
                    new Damage(40),
                    new Damage(40),
                    new Damage(50),
                    new Damage(100),
                    new Damage(150),
                }),
                new IntAbilityModification(_targetProjectileCount, new IReadOnlyParam<int>[]
                {
                    new ProjectileCount(1),
                    new ProjectileCount(1),
                    new ProjectileCount(1),
                    new ProjectileCount(1),
                    new ProjectileCount(2),
                    new ProjectileCount(2),
                    new ProjectileCount(2),
                    new ProjectileCount(2),
                }),
        });
    }

    public float ProjectileSpeed => _projectileSpeed.Value;
    public int ProjectileCount => _targetProjectileCount.Value;
    public float Damage => _targetDamage.Value;
    protected override IAbilityModification Modification => _modification;
}
