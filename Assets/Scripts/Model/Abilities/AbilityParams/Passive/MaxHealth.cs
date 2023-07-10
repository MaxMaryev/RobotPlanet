namespace BlobArena.Model
{
    public class MaxHealth : AbilityParam<float>
    {
        private const string ParamName = "Max HP";

        public MaxHealth(float value) : base(ParamName, value) { }
    }
}
