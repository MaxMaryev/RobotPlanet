using System.Collections.Generic;

namespace BlobArena.Model
{
    public abstract class PassiveAbility<T> : Ability<T> where T : class, IAbility
    {
        public PassiveAbility(string guid, string name, string description, AbilityIdentifier identifier,
            List<IAbilityListener<T>> listeners = null)
            : base(guid, name, description, AbilityType.Passive, identifier, listeners) { }
    }
}
