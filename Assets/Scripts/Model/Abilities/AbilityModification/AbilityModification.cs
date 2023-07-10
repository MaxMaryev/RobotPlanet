using System;

namespace BlobArena.Model
{
    public abstract class AbilityModification<T> : IAbilityModification
    {
        private readonly IReadOnlyParam<T>[] _parameters;
        private readonly IAbilityParam<T> _target;
        private int _currentIndex = -1;

        public AbilityModification(IAbilityParam<T> target, IReadOnlyParam<T>[] parameters)
        {
            _target = target;
            _parameters = parameters;
        }

        public bool CanUpgrade => _currentIndex + 1 < _parameters.Length;
        public int CurrentLevel => _currentIndex + 1;
        public int MaxLevel => _parameters.Length;

        public void Upgrade()
        {
            if (CanUpgrade == false)
                throw new InvalidOperationException();

            _currentIndex += 1;
            _target.SetValue(_parameters[_currentIndex].Value);
        }

        public AbilityModificationInfo[] Info()
        {
            T currentValue = _parameters[_currentIndex == -1 ? 0 : _currentIndex].Value;
            T nextValue = _parameters[_currentIndex + 1 >= _parameters.Length ? _currentIndex : _currentIndex + 1].Value;
            T upgradeValue = CalculateUpgradeValue(currentValue, nextValue);

            var info = new AbilityModificationInfo(_target.Name, Format(currentValue), Format(nextValue), Symbol(upgradeValue) + " " + Format(Abs(upgradeValue)));
            return new AbilityModificationInfo[] { info };
        }

        protected abstract T CalculateUpgradeValue(T currentValue, T nextValue);
        protected abstract string Format(T value);
        protected abstract T Abs(T value);
        protected abstract string Symbol(T upgradeValue);
    }
}
