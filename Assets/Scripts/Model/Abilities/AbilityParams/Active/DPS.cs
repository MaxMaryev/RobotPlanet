namespace BlobArena.Model
{
    public class DPS : AbilityParam<float>
    {
        private const string ParamName = "DPS";

        public DPS(float value) : base(ParamName, value) { }
    }
}
