namespace BlobArena.Model
{
    public class Damage : AbilityParam<float>
    {
        private const string ParamName = "Damage";

        public Damage(float value) : base(ParamName, value) { }
    }
}
