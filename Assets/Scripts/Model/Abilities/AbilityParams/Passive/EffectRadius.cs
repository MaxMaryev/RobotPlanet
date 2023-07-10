namespace BlobArena.Model
{
    public class EffectRadius : AbilityParam<float>
    {
        private const string ParamName = "Effect Radius";

        public EffectRadius(float value) : base(ParamName, value) { }
    }
}
