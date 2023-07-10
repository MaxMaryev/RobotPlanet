using System.Collections.Generic;

namespace BlobArena.Model
{
    public class HealthRegenerationAbility : PassiveAbility<HealthRegenerationAbility>, IUpdatable
    {
        private const string GUID = "HealthRegeneration";
        private const string Name = "Health Regeneration";
        private const string Description = "Heals a portion of health proportional to max HP";

        private readonly Timer _timer = new Timer();
        private readonly Cooldown _cooldown = new Cooldown(10);
        private readonly IAbilityModification _modification;
        private readonly HealthRegeneration _targetHealthRegeneration = new HealthRegeneration(0);
        private bool _subscribed;

        public HealthRegenerationAbility(List<IAbilityListener<HealthRegenerationAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.HealthRegeneration, listeners)
        {
            _modification = new PercentAbilityModification(_targetHealthRegeneration,
                    new IReadOnlyParam<float>[]
                    {
                        new HealthRegeneration(.01f),
                        new HealthRegeneration(.02f),
                        new HealthRegeneration(.03f),
                        new HealthRegeneration(.04f),
                        new HealthRegeneration(.06f),
                        new HealthRegeneration(.07f),
                        new HealthRegeneration(.08f),
                        new HealthRegeneration(.1f),
                    });
        }

        public float HealthRegenerationModifier => _targetHealthRegeneration.Value;
        public ITimer Timer => _timer;
        protected override IAbilityModification Modification => _modification;

        public void Tick(float tick)
        {
            _timer.Tick(tick);
        }

        protected override void OnUpgrade()
        {
            if (_subscribed == false)
            {
                _timer.Completed += OnTimerCompleted;
                _subscribed = true;
            }

            _timer.Start(_cooldown.Value);
        }

        private void OnTimerCompleted()
        {
            Use();
            _timer.Start(_cooldown.Value);
        }
    }
}
