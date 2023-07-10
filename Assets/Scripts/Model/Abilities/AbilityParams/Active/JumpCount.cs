namespace BlobArena.Model
{
    public class JumpCount : AbilityParam<int>
    {
        private const string ParamName = "Jump Count";

        public JumpCount(int value) : base(ParamName, value) { }
    }
}
