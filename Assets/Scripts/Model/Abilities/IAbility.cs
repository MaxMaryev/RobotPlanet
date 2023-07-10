namespace BlobArena.Model
{
    public interface IAbility
    {
        bool IsActive { get; }
        bool CanUpgrade { get; }
        AbilityInfo Info { get; }

        void Upgrade();
    }

    public interface IUpdatable
    {
        ITimer Timer { get; }

        void Tick(float tick);
    }

    public interface IAbilityListener<T> where T : class, IAbility
    {
        void OnAbilityUsed(T ability);
        void OnAbilityUpgrade(T ability);
    }
}
