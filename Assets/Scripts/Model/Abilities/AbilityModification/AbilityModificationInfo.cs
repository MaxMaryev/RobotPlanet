namespace BlobArena.Model
{
    public struct AbilityModificationInfo
    {
        public AbilityModificationInfo(string name, string currentValue, string nextValue, string upgradeValue)
        {
            Name = name;
            CurrentValue = currentValue;
            NextValue = nextValue;
            UpgradeValue = upgradeValue;
        }

        public string Name { get; private set; }
        public string CurrentValue { get; private set; }
        public string NextValue { get; private set; }
        public string UpgradeValue { get; private set; }
    }
}
