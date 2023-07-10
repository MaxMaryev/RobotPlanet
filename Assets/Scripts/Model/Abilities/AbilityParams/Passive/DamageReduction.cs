namespace BlobArena.Model
{
    public class DamageReduction : AbilityParam<float>
    {
        private const string ParamName = "Damage Reduction";

        public DamageReduction(float value) : base(ParamName, value) { }
    }
}
