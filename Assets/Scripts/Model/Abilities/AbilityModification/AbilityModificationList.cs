using System;
using System.Linq;
using System.Collections.Generic;

namespace BlobArena.Model
{
    public class AbilityModificationList : IAbilityModification
    {
        private readonly IAbilityModification[] _abilityModifications;
        private readonly int _maxModificationLenght;
        private int _currentIndex;

        public AbilityModificationList(IAbilityModification[] abilityModifications)
        {
            _abilityModifications = abilityModifications;
            _maxModificationLenght = abilityModifications.Max(modification => modification.MaxLevel);
        }

        public bool CanUpgrade => _currentIndex < _maxModificationLenght;
        public int CurrentLevel => _currentIndex;
        public int MaxLevel => _maxModificationLenght;

        public void Upgrade()
        {
            if (CanUpgrade == false)
                throw new InvalidOperationException();

            _currentIndex += 1;

            foreach (var modification in _abilityModifications)
                if (modification.CanUpgrade)
                    modification.Upgrade();
        }

        public AbilityModificationInfo[] Info()
        {
            List<AbilityModificationInfo> info = new List<AbilityModificationInfo>();

            foreach (var modification in _abilityModifications)
                if (modification.CanUpgrade)
                    info.AddRange(modification.Info());

            return info.ToArray();
        }
    }
}
