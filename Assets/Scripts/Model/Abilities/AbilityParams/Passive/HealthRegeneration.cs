namespace BlobArena.Model
{
    public class HealthRegeneration : AbilityParam<float>
    {
        private const string ParamName = "Health Regeneration";

        public HealthRegeneration(float value) : base(ParamName, value) { }
    }
}
