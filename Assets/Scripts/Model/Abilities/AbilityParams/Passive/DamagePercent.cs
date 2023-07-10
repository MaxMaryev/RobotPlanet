namespace BlobArena.Model
{
    public class DamagePercent : AbilityParam<float>
    {
        private const string ParamName = "Damage";

        public DamagePercent(float value) : base(ParamName, value) { }
    }
}
