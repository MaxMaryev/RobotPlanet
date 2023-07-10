namespace BlobArena.Model
{
    public struct AbilityInfo
    {
        public AbilityInfo(string name, string description, int level, AbilityType type, AbilityIdentifier identifier,
            IAbilityModification modification)
        {
            Name = name;
            Description = description;
            Level = level;
            Type = type;
            Identifier = identifier;
            Modification = modification;
            ModificationsInfo = modification.Info();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }
        public AbilityType Type { get; private set; }
        public AbilityIdentifier Identifier { get; private set; }
        public IAbilityModification Modification { get; private set; }
        public AbilityModificationInfo[] ModificationsInfo { get; private set; }
    }

    public enum AbilityType
    {
        Active,
        Passive
    }
}
