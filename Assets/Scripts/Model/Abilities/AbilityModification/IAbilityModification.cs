namespace BlobArena.Model
{
    public interface IAbilityModification
    {
        bool CanUpgrade { get; }
        int CurrentLevel { get; }
        int MaxLevel { get; }

        AbilityModificationInfo[] Info();
        void Upgrade();
    }
}
