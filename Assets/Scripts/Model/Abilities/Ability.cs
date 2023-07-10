using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlobArena.Model
{
    public abstract class Ability<T> : IAbility where T : class, IAbility
    {
        private readonly string _guid;
        private readonly string _name;
        private readonly string _description;
        private readonly AbilityType _type;
        private readonly AbilityIdentifier _identifier;
        private readonly List<IAbilityListener<T>> _listeners;

        public Ability(string guid, string name, string description, AbilityType type, AbilityIdentifier identifier,
            List<IAbilityListener<T>> listeners = null)
        {
            _guid = guid;
            _name = name;
            _description = description;
            _type = type;
            _identifier = identifier;
            _listeners = listeners;
        }

        public bool IsActive => Modification.CurrentLevel >= 1;
        public bool CanUpgrade => Modification.CanUpgrade;
        public AbilityInfo Info => new AbilityInfo(_name, _description, Modification.CurrentLevel, _type, _identifier, Modification);
        public bool IsUnlocked => PlayerPrefs.HasKey(_guid);
        protected abstract IAbilityModification Modification { get; }

        public void Upgrade()
        {
            if (CanUpgrade == false)
                throw new InvalidOperationException();

            Modification.Upgrade();
            OnUpgrade();

            _listeners?.ForEach(listener => listener.OnAbilityUpgrade(this as T));
        }

        public void Unlock()
        {
            PlayerPrefs.SetInt(_guid, 1);
        }

        public void AddListener(IAbilityListener<T> listener)
        {
            _listeners.Add(listener);
            _listeners?.ForEach(listener => listener.OnAbilityUpgrade(this as T));
        }

        protected void Use()
        {
            _listeners?.ForEach(listener => listener.OnAbilityUsed(this as T));
        }

        protected virtual void OnUpgrade() { }
    }
}
