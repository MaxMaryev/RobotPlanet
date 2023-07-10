namespace BlobArena.Model
{
    public class Cooldown : AbilityParam<float>
    {
        private const string ParamName = "Cooldown";

        public Cooldown(float value) : base(ParamName, value) { }
    }
}
