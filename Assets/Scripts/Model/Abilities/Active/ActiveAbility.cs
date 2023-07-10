using System.Collections.Generic;

namespace BlobArena.Model
{
    public abstract class ActiveAbility<T> : Ability<T>, IUpdatable where T : class, IAbility
    {
        private readonly Timer _timer = new Timer();
        private readonly Cooldown _targetCooldown = new Cooldown(0);
        private bool _subscribed;

        public ActiveAbility(string guid, string name, string description, AbilityIdentifier identifier,
            List<IAbilityListener<T>> listeners = null)
            : base(guid, name, description, AbilityType.Active, identifier, listeners) { }

        public ITimer Timer => _timer;
        public IReadOnlyParam<float> Cooldown => _targetCooldown;
        protected Cooldown TargetCooldown => _targetCooldown;

        public void Tick(float tick)
        {
            _timer.Tick(tick);
            OnTick(tick);
        }

        protected override void OnUpgrade()
        {
            if (_subscribed == false)
            {
                _timer.Completed += OnTimerCompleted;
                _subscribed = true;
            }

            _timer.Start(_targetCooldown.Value);
        }

        private void OnTimerCompleted()
        {
            Use();
            _timer.Start(_targetCooldown.Value);
        }

        protected virtual void OnTick(float tick) { }
    }
}
