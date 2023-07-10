namespace BlobArena.Model
{
    public class Radius : AbilityParam<float>
    {
        private const string ParamName = "Radius";

        public Radius(float value) : base(ParamName, value) { }
    }
}
