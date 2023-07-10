namespace BlobArena.Model
{
    public class MovementSpeed : AbilityParam<float>
    {
        private const string ParamName = "Movement Speed";

        public MovementSpeed(float value) : base(ParamName, value) { }
    }
}
