namespace BlobArena.Model
{
    public class PassThroughCount : AbilityParam<int>
    {
        private const string ParamName = "Pass Through Count";

        public PassThroughCount(int value) : base(ParamName, value) { }
    }
}

