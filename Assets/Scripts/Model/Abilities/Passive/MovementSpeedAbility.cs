using System.Collections.Generic;

namespace BlobArena.Model
{
    public class MovementSpeedAbility : PassiveAbility<MovementSpeedAbility>
    {
        private const string GUID = "MovementSpeed";
        private const string Name = "Movement Speed";
        private const string Description = "Increases player movement speed";

        private readonly IAbilityModification _modification;
        private readonly MovementSpeed _targetMovementSpeed = new MovementSpeed(0);

        public MovementSpeedAbility(List<IAbilityListener<MovementSpeedAbility>> listeners = null)
            : base(GUID, Name, Description, AbilityIdentifier.MovementSpeed, listeners)
        {
            _modification = new PercentAbilityModification(_targetMovementSpeed,
                    new IReadOnlyParam<float>[]
                    {
                        new MovementSpeed(.1f),
                        new MovementSpeed(.2f),
                        new MovementSpeed(.3f),
                        new MovementSpeed(.4f),
                        new MovementSpeed(.5f),
                        new MovementSpeed(.65f),
                        new MovementSpeed(.8f),
                        new MovementSpeed(1f),
                    });
        }

        public float MovementSpeedModifier => 1f + _targetMovementSpeed.Value;
        protected override IAbilityModification Modification => _modification;
    }
}
